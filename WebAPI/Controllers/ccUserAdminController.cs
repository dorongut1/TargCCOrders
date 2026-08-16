using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TargCCOrders.DataController;

namespace TargCCOrders.WebAPI.Controllers
{
    /// <summary>
    /// User administration for the React client, replacing the WinForms admin.
    ///
    /// Everything here is gated twice: the JWT policy gets you in the door, and
    /// IsUserManager() decides whether you may touch anyone but yourself.
    /// Never rely on the UI hiding a menu — the check lives here.
    ///
    /// Two TargCC behaviours drive the design and are easy to get wrong:
    ///
    ///  1. APPLICATION ASSIGNMENT. ccSecurity builds
    ///        "#" + user.Applications(newline -> #) + "#"
    ///     and refuses the login unless it contains "#{ApplicationName}#".
    ///     Master/ApplicationMaster are exempt — which is why the existing
    ///     accounts log in fine with a stale 'GovtReportCreator' value. Any
    ///     ordinary user created without this WILL be rejected at login with
    ///     no obvious reason, so CreateUser always writes it.
    ///
    ///  2. PASSWORD VALIDATION. Setting a password raises evtCheckPassword,
    ///     which rejects reusing the current password (fault 117) or any of the
    ///     last four (118), and enforces length per the Security /
    ///     RequireSecurePasswords system default (currently 0 => min 4 chars,
    ///     which is what lets "1234" through at all). Those faults are
    ///     translated to Hebrew below rather than shown as numbers.
    /// </summary>
    [Route("api/userAdmin")]
    [ApiController]
    [Authorize(Policy = "AdminUI")]
    public class UserAdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserAdminController(IConfiguration configuration) => _configuration = configuration;

        /// <summary>Password every reset lands on. Deliberately simple: this is an
        /// internal demo system and the user is told to change it.</summary>
        public const string DefaultResetPassword = "1234";

        private static readonly string[] ManagerRoles = { "Master", "ApplicationMaster", "UserManager" };

        // ── helpers ────────────────────────────────────────────────────────

        private static bool IsUserManager(clsRequester r) => ManagerRoles.Any(r.IsInRole);

        /// <summary>Must match what AuthController puts on the AccessingEntity,
        /// or the application check at login will fail.</summary>
        private string ApplicationName =>
            _configuration["WebAPI:ApplicationName"]
            ?? Assembly.GetEntryAssembly()?.GetName().Name
            ?? "WebAPI";

        /// <summary>Turns a TargCC fault into something a Hebrew-speaking human
        /// can act on. Falls back to the fault's own text.</summary>
        private static string Explain(clsFault fault) => fault.Number switch
        {
            117 => "הסיסמה החדשה זהה לסיסמה הנוכחית.",
            118 => "לא ניתן להשתמש באחת מ-4 הסיסמאות האחרונות של המשתמש.",
            135 => "הסיסמה חייבת להכיל לפחות 4 תווים.",
            116 => "הסיסמה אינה עומדת בדרישות המורכבות.",
            92  => "הסיסמה הנוכחית שגויה.",
            96  => "אין לך הרשאה לבצע פעולה זו.",
            _   => !string.IsNullOrEmpty(fault.FreeText) ? fault.FreeText : fault.Message
        };

        /// <summary>Minimal projection — deliberately excludes PasswordHashed and
        /// LastPasswords, which the generated UserDto does expose.</summary>
        public class AdminUserDto
        {
            public long Id { get; set; }
            public string UserName { get; set; } = "";
            public string FirstName { get; set; } = "";
            public string LastName { get; set; } = "";
            public string FullName { get; set; } = "";
            public string Email { get; set; } = "";
            public string PhoneNumber { get; set; } = "";
            public long RoleId { get; set; }
            public string RoleName { get; set; } = "";
            public bool IsDisabled { get; set; }
            public bool IsLockedOut { get; set; }
            public bool HasApplicationAccess { get; set; }
            public DateTime DatePasswordChanged { get; set; }
        }

        private AdminUserDto Project(csUser u) => new()
        {
            Id = u.ID,
            UserName = u.UserName ?? "",
            FirstName = u.FirstName ?? "",
            LastName = u.LastName ?? "",
            FullName = u.FullName ?? "",
            Email = u.Email ?? "",
            PhoneNumber = u.PhoneNumber ?? "",
            RoleId = u.RoleID,
            RoleName = u.RoleText ?? "",
            IsDisabled = u.IsDisabled,
            IsLockedOut = u.IsLockedOut,
            HasApplicationAccess = HasApp(u.Applications),
            DatePasswordChanged = u.DatePasswordChanged
        };

        /// <summary>Mirrors ccSecurity's own membership test.</summary>
        private bool HasApp(string? applications)
        {
            var apps = "#" + (applications ?? "").Replace("\r", "").Replace("\n", "#") + "#";
            return apps.IndexOf("#" + ApplicationName + "#", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>Adds this application to the user's list, preserving any others.</summary>
        private string AddApp(string? applications)
        {
            if (HasApp(applications)) return applications ?? "";
            var lines = (applications ?? "")
                .Replace("\r", "")
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            lines.Add(ApplicationName);
            return string.Join("\n", lines);
        }

        // ── read ───────────────────────────────────────────────────────────

        /// <summary>Who am I and may I manage users? Drives menu visibility.</summary>
        [HttpGet("me")]
        public ActionResult GetMe()
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            return Ok(new
            {
                userId = requester.UserID,
                userName = requester.UserName,
                fullName = requester.UserFullName,
                roles = requester.GetRoleList(),
                canManageUsers = IsUserManager(requester)
            });
        }

        [HttpGet("users")]
        public ActionResult GetUsers([FromQuery] string search = "")
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (!IsUserManager(requester))
                return Forbid();

            clsFault? fault = null;
            // TextOnly pulls the parent Role's display text (RoleText) without
            // hydrating the whole Role object — enough for the grid.
            var users = new csUserCol(clsEnums.enmLoadParent.TextOnly, requester, ref fault);
            if (fault == null || !fault.isOK) return BadRequest(new { message = Explain(fault!) });

            var items = users.Cast<csUser>()
                             .Where(u => !string.IsNullOrWhiteSpace(u.UserName))
                             .Select(Project);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLowerInvariant();
                items = items.Where(u => u.UserName.ToLowerInvariant().Contains(s)
                                      || u.FullName.ToLowerInvariant().Contains(s)
                                      || u.Email.ToLowerInvariant().Contains(s));
            }

            return Ok(new { items = items.OrderBy(u => u.UserName).ToList() });
        }

        [HttpGet("roles")]
        public ActionResult GetRoles()
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (!IsUserManager(requester)) return Forbid();

            clsFault? fault = null;
            var roles = new csRoleCol(clsEnums.enmLoadParent.DoNotLoad, requester, ref fault);
            if (fault == null || !fault.isOK) return BadRequest(new { message = Explain(fault!) });

            return Ok(roles.Cast<csRole>()
                           .Select(r => new { id = r.ID, name = r.Name })
                           .OrderBy(r => r.name)
                           .ToList());
        }

        // ── write ──────────────────────────────────────────────────────────

        public class CreateUserRequest
        {
            public string UserName { get; set; } = "";
            public string FirstName { get; set; } = "";
            public string LastName { get; set; } = "";
            public string Email { get; set; } = "";
            public string PhoneNumber { get; set; } = "";
            public long RoleId { get; set; }
        }

        [HttpPost("users")]
        public ActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (!IsUserManager(requester)) return Forbid();

            if (string.IsNullOrWhiteSpace(request?.UserName))
                return BadRequest(new { message = "שם משתמש הוא שדה חובה." });
            if (request.RoleId <= 0)
                return BadRequest(new { message = "יש לבחור תפקיד." });

            // Reject duplicates up front — the DB would fail with a raw index error.
            var existing = new csUser(clsEnums.enmLoadParent.DoNotLoad);
            var lookup = existing.GetByUserName(request.UserName.Trim(), requester);
            if (lookup != null && lookup.isOK && existing.ID > 0)
                return Conflict(new { message = $"שם המשתמש '{request.UserName.Trim()}' כבר קיים." });

            var user = new csUser(clsEnums.enmLoadParent.DoNotLoad)
            {
                UserName = request.UserName.Trim(),
                FirstName = request.FirstName?.Trim() ?? "",
                LastName = request.LastName?.Trim() ?? "",
                Email = request.Email?.Trim() ?? "",
                PhoneNumber = request.PhoneNumber?.Trim() ?? "",
                RoleID = request.RoleId,
                IsDisabled = false,
                IsLockedOut = false,
                PasswordNeverExpires = true
            };

            var fault = user.Update(requester);
            if (!fault.isOK) return BadRequest(new { message = Explain(fault) });

            // Order matters: the user must exist (and have an ID, used as the
            // password salt) before the password and application can be set.
            var appFault = user.UpdateApplications(AddApp(user.Applications), requester);
            var pwdFault = user.UpdatePasswordHashed(DefaultResetPassword, requester);

            var warnings = new List<string>();
            if (!appFault.isOK)
                warnings.Add("שיוך לאפליקציה נכשל — המשתמש לא יוכל להתחבר: " + Explain(appFault));
            if (!pwdFault.isOK)
                warnings.Add("קביעת הסיסמה ההתחלתית נכשלה: " + Explain(pwdFault));

            return Ok(new
            {
                user = Project(user),
                initialPassword = pwdFault.isOK ? DefaultResetPassword : null,
                message = pwdFault.isOK
                    ? $"המשתמש נוצר. הסיסמה ההתחלתית היא {DefaultResetPassword} — ניתן לשנות אותה במסך ניהול המשתמשים."
                    : "המשתמש נוצר, אך ללא סיסמה תקינה.",
                warnings
            });
        }

        [HttpPost("users/{id}/resetPassword")]
        public ActionResult ResetPassword(long id)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (!IsUserManager(requester)) return Forbid();

            var user = new csUser(clsEnums.enmLoadParent.DoNotLoad);
            var fault = user.GetByID(id, requester, vMustExist: true);
            if (!fault.isOK) return NotFound(new { message = "המשתמש לא נמצא." });

            fault = user.UpdatePasswordHashed(DefaultResetPassword, requester);

            // 117/118: the password already IS 1234, or it is in the user's last
            // four. Either way the end state is what the manager wanted, so this
            // is reported as success with an explanatory note rather than a failure.
            if (!fault.isOK && (fault.Number == 117 || fault.Number == 118))
                return Ok(new
                {
                    message = $"הסיסמה של {user.UserName} כבר {DefaultResetPassword} — לא בוצע שינוי.",
                    password = DefaultResetPassword,
                    changed = false
                });

            if (!fault.isOK) return BadRequest(new { message = Explain(fault) });

            return Ok(new
            {
                message = $"סיסמתו של {user.UserName} שונתה ל-{DefaultResetPassword}. ניתן לשנות אותה במסך ניהול המשתמשים.",
                password = DefaultResetPassword,
                changed = true
            });
        }

        [HttpPost("users/{id}/unlock")]
        public ActionResult Unlock(long id)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (!IsUserManager(requester)) return Forbid();

            var user = new csUser(clsEnums.enmLoadParent.DoNotLoad);
            var fault = user.GetByID(id, requester, vMustExist: true);
            if (!fault.isOK) return NotFound(new { message = "המשתמש לא נמצא." });

            user.IsLockedOut = false;
            user.IsDisabled = false;
            fault = user.Update(requester);
            if (!fault.isOK) return BadRequest(new { message = Explain(fault) });

            return Ok(new { message = $"החסימה של {user.UserName} הוסרה.", user = Project(user) });
        }

        public class ChangeMyPasswordRequest
        {
            public string CurrentPassword { get; set; } = "";
            public string NewPassword { get; set; } = "";
        }

        /// <summary>Self-service. Intentionally NOT gated on IsUserManager —
        /// every user may change their own password, and only their own:
        /// the target is taken from the token, never from the request body.</summary>
        [HttpPost("changeMyPassword")]
        public ActionResult ChangeMyPassword([FromBody] ChangeMyPasswordRequest request)
        {
            clsRequester requester;
            try { requester = RequesterFactory.FromUser(User); }
            catch (Exception ex) { return Unauthorized(new { message = ex.Message }); }

            if (string.IsNullOrEmpty(request?.NewPassword))
                return BadRequest(new { message = "יש להזין סיסמה חדשה." });
            if (request.NewPassword.Trim().Length < 4)
                return BadRequest(new { message = "הסיסמה חייבת להכיל לפחות 4 תווים." });

            var user = new csUser(clsEnums.enmLoadParent.DoNotLoad);
            var fault = user.GetByID(requester.UserID, requester, vMustExist: true);
            if (!fault.isOK) return BadRequest(new { message = Explain(fault) });

            // Prove ownership before changing anything — a stolen token should not
            // be enough to take over the account.
            //
            // csUser.CheckPassword is NOT usable here. It compares a plain
            // unsalted SHA256 against the stored hash (which the login path does
            // not produce), and worse, when the comparison fails it sets fault 92
            // and then unconditionally calls pFault.SetOK() in its expiry branch,
            // which throws — turning a wrong password into a 500.
            //
            // Re-authenticating opens a NEW login session. With simultaneous
            // logins disabled that supersedes the caller's own session, and the
            // original requester is then rejected with
            // "Expected LoginID x, Found LoginID y". So the fresh requester the
            // re-auth produced is what must carry out the change.
            var verified = ReAuthenticate(user.UserName, request.CurrentPassword ?? "");
            if (verified == null)
                return BadRequest(new { message = "הסיסמה הנוכחית שגויה." });

            fault = user.ChangePassword(request.NewPassword, verified);
            if (!fault.isOK) return BadRequest(new { message = Explain(fault) });

            return Ok(new { message = "הסיסמה שונתה בהצלחה." });
        }

        /// <summary>Verifies a username/password pair via TargCC's own login and
        /// returns the resulting requester, or null when the password is wrong.</summary>
        private clsRequester? ReAuthenticate(string userName, string password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            try
            {
                clsFault initFault = null!;
                var accessingEntity = new csAccessingEntity(
                    vLoadPCDetails: false, vLoadIPAndCountry: false,
                    vRequester: null, rFault: ref initFault);
                accessingEntity.ApplicationName = ApplicationName;
                accessingEntity.DnsGetHostName = Environment.MachineName;
                accessingEntity.EnvironmentUserName = Environment.UserName;
                accessingEntity.GmtTime = DateTime.UtcNow;
                accessingEntity.LocalTime = DateTime.Now;

                clsRequester probe = null!;
                var fault = ccSecurity.LogInByNamePwd(userName, password, ref probe,
                    vSendMessageFor2FA: false, vSendMessageOnPasswordExpiry: false,
                    vAccessingEntity: accessingEntity);
                return fault.isOK ? probe : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
