using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.IdentityModel.Tokens; 
using System; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text; 
using TargCCOrders.DataController; 
 
namespace TargCCOrders.WebAPI.Controllers 
{ 
    [Route("api/auth")] 
    [ApiController] 
    public class AuthController : ControllerBase 
    { 
        private readonly IConfiguration _configuration; 
 
        public AuthController(IConfiguration configuration) 
        { 
            _configuration = configuration; 
        } 
 
        // POST api/auth/login
        [HttpPost("login")]
        [Microsoft.AspNetCore.RateLimiting.EnableRateLimiting("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        { 
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)) 
                return BadRequest(new { message = "Username and password are required" }); 
 
            // Build AccessingEntity for web context (required by TargCC in web apps) 
            clsFault initFault = null!; 
            var accessingEntity = new csAccessingEntity(vLoadPCDetails: false, vLoadIPAndCountry: false, vRequester: null, rFault: ref initFault); 
            accessingEntity.ApplicationName = _configuration["WebAPI:ApplicationName"] ?? System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name ?? "WebAPI"; 
            accessingEntity.AccessingComputerDetails = HttpContext.Request.Headers["User-Agent"].ToString(); 
            accessingEntity.ClientReportedIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""; 
            accessingEntity.WSReportedIP = HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""; 
            accessingEntity.DnsGetHostName = Environment.MachineName; 
            accessingEntity.EnvironmentUserName = Environment.UserName; 
            accessingEntity.GmtTime = DateTime.UtcNow; 
            accessingEntity.LocalTime = DateTime.Now; 
 
            // Use TargCC built-in login - handles all password formats, lockout, OTP, etc. 
            clsRequester requester = null!; 
            clsFault fault = ccSecurity.LogInByNamePwd(request.Username, request.Password, ref requester, 
                vSendMessageFor2FA: false, vSendMessageOnPasswordExpiry: false, 
                vAccessingEntity: accessingEntity); 
 
            if (!fault.isOK)
                return Unauthorized(new {
                    message = !string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : "Invalid username or password",
                    faultNumber = fault.Number,
                    faultMessage = fault.Message
                });

            // Generate JWT token using info from the authenticated requester.
            // The TargCC ticket (encrypted by DBController) is embedded so every
            // subsequent request runs as the REAL user — permissions + audit intact.
            var jwtKey = _configuration["Jwt:AdminKey"]
                ?? throw new InvalidOperationException("Missing Jwt:AdminKey in configuration");
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, requester.UserID.ToString()),
                    new Claim(ClaimTypes.Name, requester.UserName ?? request.Username),
                    new Claim("FullName", requester.UserFullName ?? requester.UserName ?? request.Username),
                    new Claim("UserArea", "Admin"),
                    new Claim(RequesterFactory.TicketClaim, requester.CreateTicket()),
                }),
                Expires = DateTime.UtcNow.AddHours(8), 
                SigningCredentials = new SigningCredentials( 
                    new SymmetricSecurityKey(keyBytes), 
                    SecurityAlgorithms.HmacSha256Signature 
                ), 
                Issuer = "CodeCreator", 
                Audience = "AdminUI" 
            }; 
            var token = tokenHandler.CreateToken(tokenDescriptor); 
 
            return Ok(new { token = tokenHandler.WriteToken(token) }); 
        } 
 
        // POST api/auth/refresh — issue new token from valid existing token 
        [HttpPost("refresh")] 
        [Authorize(Policy = "AdminUI")] 
        public ActionResult RefreshToken() 
        { 
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var fullName = User.FindFirst("FullName")?.Value;
            var ticket = User.FindFirst(RequesterFactory.TicketClaim)?.Value;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(ticket)) return Unauthorized();

            var jwtKey = _configuration["Jwt:AdminKey"]
                ?? throw new InvalidOperationException("Missing Jwt:AdminKey in configuration");
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId ?? "0"),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim("FullName", fullName ?? userName),
                    new Claim("UserArea", "Admin"),
                    new Claim(RequesterFactory.TicketClaim, ticket),
                }),
                Expires = DateTime.UtcNow.AddHours(8), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature), 
                Issuer = "CodeCreator", 
                Audience = "AdminUI" 
            }; 
            var token = tokenHandler.CreateToken(tokenDescriptor); 
            return Ok(new { token = tokenHandler.WriteToken(token) }); 
        } 
 
    } 
 
    public class LoginRequest 
    { 
        public string Username { get; set; } = ""; 
        public string Password { get; set; } = ""; 
    } 
 
    public class BatchPatchRequest
    {
        public long[] Ids { get; set; } = Array.Empty<long>();
        public Dictionary<string, object?> Fields { get; set; } = new();
    }
} 
