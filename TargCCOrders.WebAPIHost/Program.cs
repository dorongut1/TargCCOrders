using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

// ═══════════════════════════════════════════════════════════════════
// TargCCOrders WebAPI Host – ASP.NET Core 8
// Bootstraps the generated Controllers + DTOs from WebAPI/ library
// ═══════════════════════════════════════════════════════════════════

var builder = WebApplication.CreateBuilder(args);

// ──────────── Serilog ────────────
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/api-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
    .CreateLogger();

builder.Host.UseSerilog();

// ──────────── Configuration ────────────
var jwtKey = builder.Configuration["Jwt:AdminKey"]
    ?? throw new InvalidOperationException("Missing Jwt:AdminKey in configuration");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

// ──────────── Services ────────────

// Controllers + JSON
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = true,
            ValidIssuer = "CodeCreator",
            ValidateAudience = true,
            ValidAudience = "AdminUI",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    });

// Authorization policy used by controllers
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminUI", policy => policy.RequireAuthenticatedUser());
});

// CORS – allow React dev server + production origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev", policy =>
    {
        var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? new[] { "http://localhost:5173", "http://localhost:3000" };

        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithExposedHeaders("ETag", "X-Total-Count");
    });
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TargCCOrders API",
        Version = "v1",
        Description = "REST API for TargCCOrders management system"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Response compression
builder.Services.AddResponseCompression(opts =>
{
    opts.EnableForHttps = true;
});

// Rate limiting — protects the login endpoint from brute-force
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.AddPolicy("login", context =>
        System.Threading.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new System.Threading.RateLimiting.FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

// ──────────── DB Connection (VB.NET layer) ────────────
// The VB.NET DBController builds its own connection string from the
// app.config appSetting "TargCCOrders.Controller" (Server~Database[~MaxPool[~User~Pwd]]).
// The ConnectionStrings section in appsettings.json is NOT used by the data layer.
// Fail fast at startup if the setting is missing so misconfiguration is obvious:
{
    var controllerSetting = System.Configuration.ConfigurationManager.AppSettings["TargCCOrders.Controller"];
    if (string.IsNullOrWhiteSpace(controllerSetting))
        throw new InvalidOperationException(
            "Missing appSetting 'TargCCOrders.Controller' in app.config — the DBController cannot connect to the database without it.");
    Log.Information("DBController target: {Controller}", controllerSetting.Split('~')[0] + "~" + controllerSetting.Split('~').ElementAtOrDefault(1));
}

var app = builder.Build();

// ──────────── Middleware Pipeline ────────────

// Global exception handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (error != null)
        {
            Log.Error(error.Error, "Unhandled exception");
            await context.Response.WriteAsJsonAsync(new
            {
                message = "An internal error occurred. Please try again.",
                traceId = context.TraceIdentifier
            });
        }
    });
});

// Swagger (dev + staging only)
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TargCCOrders API v1");
        c.RoutePrefix = "swagger";
    });
}

// HTTPS enforcement (production)
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    await next();
});

app.UseResponseCompression();

// Serve React SPA from wwwroot (production build)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowReactDev");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// SPA fallback – return index.html for unmatched routes (React Router)
app.MapFallbackToFile("index.html");

Log.Information("TargCCOrders API starting on {Urls}", string.Join(", ", app.Urls));
app.Run();
