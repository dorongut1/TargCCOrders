<#
    Check-Prerequisites.ps1 — preflight for the TargCCOrders server

    Run ON THE SERVER, in an elevated PowerShell, BEFORE deploying.

    Simplest usage — drop this file into the site folder and run it there:
        cd D:\Webs\Orders.target.co.il
        powershell -ExecutionPolicy Bypass -File .\Check-Prerequisites.ps1

    PublishPath defaults to the folder this script sits in, so nothing needs
    to be passed when it is placed alongside the application.

    Read-only: it inspects and reports, it never installs or changes anything.

    Optional switches (write them on ONE line, and with no angle brackets):
        -PublishPath  D:\Webs\Orders.target.co.il
        -SqlServer    SQLSRV01          or  localhost  when SQL is on this box
        -Database     TargCCOrdersNew
        -AppPoolName  Orders.target.co.il
#>
[CmdletBinding()]
param(
    [string]$PublishPath = $PSScriptRoot,
    [string]$SqlServer   = 'localhost',
    [string]$Database    = 'TargCCOrdersNew',
    [string]$AppPoolName = 'TargCCOrders'
)

if ([string]::IsNullOrWhiteSpace($PublishPath)) { $PublishPath = (Get-Location).Path }

$script:fail = 0
$script:warn = 0

function Ok   ($m) { Write-Host "  [ OK ]   $m" -ForegroundColor Green }
function Bad  ($m, $fix) {
    Write-Host "  [FAIL]   $m" -ForegroundColor Red
    if ($fix) { Write-Host "           -> $fix" -ForegroundColor Yellow }
    $script:fail++
}
function Warn ($m, $fix) {
    Write-Host "  [WARN]   $m" -ForegroundColor DarkYellow
    if ($fix) { Write-Host "           -> $fix" -ForegroundColor Yellow }
    $script:warn++
}
function Section($t) { Write-Host "`n=== $t ===" -ForegroundColor Cyan }

Write-Host "TargCCOrders — server preflight" -ForegroundColor White
Write-Host ("Host: {0}   {1}" -f $env:COMPUTERNAME, (Get-Date -Format 'yyyy-MM-dd HH:mm'))

Section '1. Operating system and elevation'
$os = Get-CimInstance Win32_OperatingSystem
Ok ("$($os.Caption)  build $($os.BuildNumber)")
if ($os.ProductType -eq 1) {
    Warn 'This is a client Windows, not Windows Server.' 'Fine for an internal demo; IIS features differ slightly.'
}
$elevated = ([Security.Principal.WindowsPrincipal] `
    [Security.Principal.WindowsIdentity]::GetCurrent()
    ).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if ($elevated) { Ok 'Running elevated' }
else { Warn 'Not elevated — some checks will report nothing.' 'Re-run PowerShell as Administrator.' }

Section '2. .NET 8 Hosting Bundle'
# The Hosting Bundle is what installs the ASP.NET Core Module into IIS. The plain
# Runtime or the SDK is NOT enough: the app would fail with HTTP 500.19.
$runtimes = & dotnet --list-runtimes 2>$null
if (-not $runtimes) {
    Bad 'dotnet not found on PATH.' 'Install the .NET 8 Hosting Bundle, then run: iisreset'
} else {
    $aspnet8 = $runtimes | Where-Object { $_ -match '^Microsoft\.AspNetCore\.App 8\.' }
    if ($aspnet8) { Ok ('ASP.NET Core 8 runtime: ' + (($aspnet8 | Select-Object -First 1) -split ' ')[1]) }
    else { Bad 'Microsoft.AspNetCore.App 8.x is missing.' 'Install the .NET 8 Hosting Bundle (not the SDK), then: iisreset' }
}

# The module has not lived in System32\inetsrv for several releases: it installs
# under Program Files and IIS references it by path. Asking IIS what it has
# registered is the only reliable test — checking a file path reports a healthy
# server as broken.
$ancmRegistered = $false
try {
    Import-Module WebAdministration -ErrorAction Stop
    $ancmRegistered = [bool](Get-WebGlobalModule | Where-Object Name -eq 'AspNetCoreModuleV2')
} catch { }

if ($ancmRegistered) { Ok 'AspNetCoreModuleV2 is registered in IIS' }
else {
    $onDisk = @(
        "$env:ProgramFiles\IIS\Asp.Net Core Module\V2\aspnetcorev2.dll",
        "$env:windir\System32\inetsrv\aspnetcorev2.dll"
    ) | Where-Object { Test-Path $_ }
    if ($onDisk) {
        Bad 'The module is on disk but not registered with IIS.' 'Repair the Hosting Bundle: <installer>.exe /repair /quiet, then iisreset'
    } else {
        Bad 'ASP.NET Core Module V2 not found.' 'Install the .NET 8 Hosting Bundle (not the SDK, not the plain runtime), then: iisreset'
    }
}

Section '3. IIS'
$w3 = Get-Service W3SVC -ErrorAction SilentlyContinue
if ($w3) { Ok ("W3SVC service is $($w3.Status)") }
else { Bad 'IIS (W3SVC) is not installed.' 'Server Manager -> Add Roles -> Web Server (IIS). Install the Hosting Bundle AFTER this.' }

if (Get-Module -ListAvailable -Name WebAdministration) {
    Import-Module WebAdministration -ErrorAction SilentlyContinue
    Ok 'WebAdministration module available'
    $pool = Get-Item "IIS:\AppPools\$AppPoolName" -ErrorAction SilentlyContinue
    if ($pool) {
        Ok "App pool '$AppPoolName' exists"
        # Counter-intuitive but correct: a .NET 8 app runs OUT of the CLR, hosted
        # by the ASP.NET Core Module, so the pool must be set to No Managed Code.
        if ($pool.managedRuntimeVersion -eq '') { Ok "  managedRuntimeVersion = No Managed Code (correct for .NET 8)" }
        else { Bad "  managedRuntimeVersion = '$($pool.managedRuntimeVersion)'" "Set the pool's .NET CLR version to 'No Managed Code'." }
    } else {
        Warn "App pool '$AppPoolName' does not exist yet." 'Create it during deployment (step 7 of DEPLOY_TO_SERVER.md).'
    }
} else {
    Warn 'WebAdministration module not available — skipping app pool checks.' 'Install IIS Management Scripts and Tools.'
}

Section '4. .NET Framework 4.8 (required by DBController)'
$rel = (Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' -ErrorAction SilentlyContinue).Release
if ($rel -ge 528040) { Ok ".NET Framework 4.8 or later (Release $rel)" }
elseif ($rel)        { Bad ".NET Framework release $rel is older than 4.8." 'Install .NET Framework 4.8.' }
else                 { Bad '.NET Framework 4.x not detected.' 'Install .NET Framework 4.8.' }

Section "5. SQL Server ($SqlServer / $Database)"
$conn = New-Object System.Data.SqlClient.SqlConnection
$conn.ConnectionString = "Server=$SqlServer;Database=$Database;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=8"
try {
    $conn.Open()
    Ok "Connected to $SqlServer, database $Database"

    $cmd = $conn.CreateCommand()

    # The restore is only useful if it carries the repairs, so check for the
    # objects that prove it: the VAT helper, and a non-zero VAT rate on orders.
    $cmd.CommandText = "SELECT CASE WHEN OBJECT_ID('dbo.fnCurrentVATRate') IS NULL THEN 0 ELSE 1 END"
    if ([int]$cmd.ExecuteScalar() -eq 1) { Ok '  fnCurrentVATRate present (VAT fix is in this database)' }
    else { Bad '  fnCurrentVATRate is missing.' 'The backup predates the VAT fix. Re-take it from the dev machine.' }

    $cmd.CommandText = 'SELECT COUNT(*) FROM dbo.OrderHeader WHERE ISNULL(VATRatePercent,0)=0'
    $zeroVat = [int]$cmd.ExecuteScalar()
    if ($zeroVat -eq 0) { Ok '  every order carries a VAT rate' }
    else { Bad "  $zeroVat orders still have VATRatePercent = 0." 'Run Database\FIX_VATRate_ServerSide_2026-08-16.sql (sqlcmd -I).' }

    foreach ($t in 'Customer','Product','ProductPrice') {
        $cmd.CommandText = "SELECT COUNT(*) FROM dbo.$t"
        $n = [int]$cmd.ExecuteScalar()
        if ($n -gt 0) { Ok "  $t rows: $n" } else { Warn "  $t is empty" 'Master data was not imported into this database.' }
    }

    # Ordinal-drift guard: AddedOn must precede RivhitCustomerNo in the Customer
    # select, otherwise /api/customers returns 400 at runtime.
    $drift = "SELECT CASE WHEN CHARINDEX('[RivhitCustomerNo]', OBJECT_DEFINITION(OBJECT_ID('dbo.ccCustomersFill')))" +
             " > CHARINDEX('[AddedOn]', OBJECT_DEFINITION(OBJECT_ID('dbo.ccCustomersFill')))" +
             " THEN 1 ELSE 0 END"
    $cmd.CommandText = $drift
    if ([int]$cmd.ExecuteScalar() -eq 1) { Ok '  customer procedure column order is correct' }
    else { Bad '  ccCustomersFill has the old column order.' 'Run Database\FIX_ProcOrdinalDrift_2026-08-17.sql (sqlcmd -I).' }

    # TargCC refuses to even BUILD a connection string when SQLCLR is off:
    #   "This application requires CLR to be enabled. Please contact your DBA."
    # It is raised in MyController.CreateDBConnString, before any query runs, so
    # the symptom is every endpoint failing while the startup log still reports a
    # healthy connection. Off by default on a fresh SQL instance.
    $cmd.CommandText = "SELECT CAST(value_in_use AS int) FROM sys.configurations WHERE name = 'clr enabled'"
    $clr = $cmd.ExecuteScalar()
    if ([int]$clr -eq 1) { Ok '  SQLCLR is enabled' }
    else {
        Bad '  SQLCLR is disabled — TargCC cannot build a connection string.' `
            "EXEC sp_configure 'show advanced options',1; RECONFIGURE; EXEC sp_configure 'clr enabled',1; RECONFIGURE;"
    }

    $cmd.CommandText = "SELECT CAST(value_in_use AS int) FROM sys.configurations WHERE name = 'clr strict security'"
    $strict = $cmd.ExecuteScalar()
    if ($null -ne $strict -and [int]$strict -eq 1) {
        Warn '  clr strict security is on — the TargCC audit assembly may be blocked.' `
             'If auditing misbehaves: ALTER DATABASE TargCCOrdersNew SET TRUSTWORTHY ON; EXEC sp_changedbowner ''sa'';'
    }

    $conn.Close()
} catch {
    Bad ('Cannot connect: ' + $_.Exception.Message) 'Check the instance name, that SQL is running, and that this account may connect.'
}

Section "6. Deployment folder ($PublishPath)"
if (-not (Test-Path $PublishPath)) {
    Warn 'Folder does not exist yet.' 'Copy the publish output here, then re-run this script.'
} else {
    $required = @{
        'TargCCOrders.WebAPIHost.dll'        = 'the application itself'
        'TargCCOrders.WebAPIHost.dll.config' = 'connection string and LogLocation'
        'appsettings.json'                   = 'CORS and JWT settings'
        'web.config'                         = 'IIS hosting; without it the site will not start'
        'TargCCOrders.DBController.dll'      = 'the TargCC data layer'
        # Not optional: DBController calls WMI while building the AccessingEntity.
        # Missing it compiles fine and then fails EVERY login with fault 60.
        'System.Management.dll'              = 'WMI — every login fails without it'
        'wwwroot\index.html'                 = 'the React build'
    }
    foreach ($f in $required.Keys) {
        if (Test-Path (Join-Path $PublishPath $f)) { Ok $f }
        else { Bad "$f is missing ($($required[$f]))" 'Re-run the publish, and remember to copy dist\* into wwwroot.' }
    }

    $cfg = Join-Path $PublishPath 'TargCCOrders.WebAPIHost.dll.config'
    if (Test-Path $cfg) {
        $txt = Get-Content $cfg -Raw
        # Three segments means Integrated Security, which is a legitimate choice:
        # the app pool identity connects and no password sits in a text file.
        if ($txt -match 'key="TargCCOrders\.Controller"\s+value="([^"]+)"') {
            $cs = $Matches[1]
            $parts = $cs.Split('~')
            if ($parts.Count -ge 5)      { Ok "  connection string uses SQL authentication (user $($parts[3]))" }
            elseif ($parts.Count -eq 3)  { Ok "  connection string uses Integrated Security ($($parts[0])/$($parts[1]))" }
            else                         { Bad "  connection string looks malformed: $cs" 'Expected Server~Database~Timeout[~User~Password].' }
        } else {
            Bad '  TargCCOrders.Controller not found in the config.' 'Add it, or the application cannot reach the database.'
        }

        if ($txt -match 'key="LogLocation"\s+value="([^"]+)"') {
            $log = $Matches[1]
            if (Test-Path $log) {
                try {
                    $probe = Join-Path $log ('_w_' + [guid]::NewGuid().ToString('N') + '.tmp')
                    [IO.File]::WriteAllText($probe, 'x'); Remove-Item $probe -Force
                    Ok "  LogLocation $log exists and is writable"
                } catch {
                    Bad "  LogLocation $log is not writable." "icacls `"$log`" /grant `"IIS AppPool\$AppPoolName`:(OI)(CI)M`""
                }
            } else {
                Bad "  LogLocation $log does not exist." "New-Item -ItemType Directory -Path '$log' -Force"
            }
        }
    }
}

Section '7. Secrets and environment'
$jwt = [Environment]::GetEnvironmentVariable('Jwt__AdminKey', 'Machine')
if ([string]::IsNullOrWhiteSpace($jwt)) {
    Bad 'Jwt__AdminKey machine environment variable is not set.' `
        "New key:  [Convert]::ToBase64String((1..48 | % { Get-Random -Maximum 256 }))   then SetEnvironmentVariable('Jwt__AdminKey',<key>,'Machine') and iisreset"
} elseif ($jwt.Length -lt 32) {
    Bad "Jwt__AdminKey is only $($jwt.Length) characters." 'Use 32 or more.'
} elseif ($jwt -like 'CHANGE-ME*') {
    Bad 'Jwt__AdminKey still holds the placeholder.' 'Generate a real key.'
} else {
    Ok "Jwt__AdminKey is set ($($jwt.Length) characters)"
}

$aspenv = [Environment]::GetEnvironmentVariable('ASPNETCORE_ENVIRONMENT', 'Machine')
if ($aspenv -eq 'Production') { Ok 'ASPNETCORE_ENVIRONMENT = Production' }
elseif ($aspenv)              { Warn "ASPNETCORE_ENVIRONMENT = $aspenv" 'Use Production to disable Swagger and enable HSTS.' }
else                          { Warn 'ASPNETCORE_ENVIRONMENT not set at machine level.' 'Set it here or per-site in IIS Configuration Editor.' }

Section '8. Summary'
if ($script:fail -eq 0 -and $script:warn -eq 0) {
    Write-Host "  Everything checks out. Proceed with the deployment." -ForegroundColor Green
} elseif ($script:fail -eq 0) {
    Write-Host "  No blockers. $($script:warn) warning(s) — review them, then proceed." -ForegroundColor DarkYellow
} else {
    Write-Host "  $($script:fail) blocker(s) and $($script:warn) warning(s). Fix the blockers before deploying." -ForegroundColor Red
}
Write-Host ""
exit $script:fail
