# Build-Package.ps1 - build the React client and the .NET host, assemble a
# clean deployment folder, and zip it for transfer to the server.
#
# The package deliberately excludes the configuration files. On the server
# those hold the production connection string and log location, and the ones
# produced by a build hold neither -- overwriting them takes the site down.
#
# Usage:   .\Build-Package.ps1
#          .\Build-Package.ps1 -SkipReact        (host only)
#          .\Build-Package.ps1 -NoZip            (leave the folder, no archive)

[CmdletBinding()]
param(
    [string] $Configuration = 'Release',
    [string] $StageRoot     = 'C:\Dev\Publish\TargCCOrders',
    [string] $ZipRoot       = 'C:\Dev\Publish',
    [string] $MSBuild       = 'C:\Program Files\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin\MSBuild.exe',
    [switch] $SkipReact,
    [switch] $NoZip
)

$ErrorActionPreference = 'Stop'

# Files that must never leave the build machine. The server's copies carry the
# production connection string and LogLocation; app.config is a build artefact
# that .NET 8 does not read at runtime but that invites confusion on a server.
$ExcludedConfigs = @(
    'TargCCOrders.WebAPIHost.dll.config',
    'web.config',
    'app.config'
)

$RepoRoot   = Split-Path -Parent $PSScriptRoot
$ReactDir   = Join-Path $RepoRoot 'TargCCOrders.ReactUI\ReactUI'
$HostProj   = Join-Path $RepoRoot 'TargCCOrders.WebAPIHost\TargCCOrders.WebAPIHost.csproj'
$DistDir    = Join-Path $ReactDir 'dist'
$WwwRoot    = Join-Path $StageRoot 'wwwroot'

function Step([string] $Text) {
    Write-Host ''
    Write-Host "==> $Text" -ForegroundColor Cyan
}

function Fail([string] $Text) {
    Write-Host ''
    Write-Host "FAILED: $Text" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $HostProj)) { Fail "Host project not found at $HostProj" }
if (-not (Test-Path $MSBuild))  { Fail "MSBuild not found at $MSBuild. Pass -MSBuild <path>." }

# ---------------------------------------------------------------- React ----
if (-not $SkipReact) {
    Step 'Type-checking the React client'
    Push-Location $ReactDir
    try {
        & cmd /c "npx tsc --noEmit 2>&1"
        if ($LASTEXITCODE -ne 0) { Fail 'TypeScript reported errors. Nothing was packaged.' }

        Step 'Running the unit tests'
        & cmd /c "npx vitest run 2>&1"
        if ($LASTEXITCODE -ne 0) { Fail 'Tests failed. Nothing was packaged.' }

        # npm returns a non-zero code through PowerShell even on success, so the
        # build is run through cmd and judged on cmd's own exit code.
        Step 'Building the React client'
        & cmd /c "npm run build 2>&1"
        if ($LASTEXITCODE -ne 0) { Fail 'React build failed.' }
    }
    finally { Pop-Location }

    if (-not (Test-Path $DistDir)) { Fail "React build produced no dist folder at $DistDir" }
}

# ----------------------------------------------------------------- .NET ----
Step "Publishing the host ($Configuration)"
& $MSBuild $HostProj -t:Publish -p:Configuration=$Configuration -p:PublishDir="$StageRoot\" -v:m
if ($LASTEXITCODE -ne 0) { Fail 'MSBuild publish failed.' }

# ------------------------------------------------------------- Assemble ----
# Publish does not copy the React output, and a plain overwrite leaves the
# previous build's hashed assets behind -- which is how a server ends up
# serving a stale bundle nobody can find in the source.
Step 'Refreshing wwwroot from the React build'
if (Test-Path $WwwRoot) { Remove-Item $WwwRoot -Recurse -Force }
New-Item -ItemType Directory -Force -Path $WwwRoot | Out-Null
Copy-Item (Join-Path $DistDir '*') $WwwRoot -Recurse -Force

Step 'Removing configuration files from the package'
foreach ($name in $ExcludedConfigs) {
    $path = Join-Path $StageRoot $name
    if (Test-Path $path) {
        Remove-Item $path -Force
        Write-Host "    removed $name"
    }
}

# ---------------------------------------------------------------- Check ----
Step 'Checking the package'

$indexes = @(Get-ChildItem (Join-Path $WwwRoot 'assets') -Filter 'index-*.js' -ErrorAction SilentlyContinue)
if ($indexes.Count -ne 1) {
    Fail "Expected exactly one index-*.js in wwwroot\assets, found $($indexes.Count). The client bundle is not clean."
}

foreach ($name in $ExcludedConfigs) {
    if (Test-Path (Join-Path $StageRoot $name)) { Fail "$name is still in the package." }
}

if (-not (Test-Path (Join-Path $StageRoot 'TargCCOrders.WebAPIHost.dll'))) {
    Fail 'The host assembly is missing from the package.'
}

$fileCount = (Get-ChildItem $StageRoot -Recurse -File).Count
Write-Host "    $fileCount files, bundle $($indexes[0].Name)" -ForegroundColor Green

# ------------------------------------------------------------------ Zip ----
if (-not $NoZip) {
    $stamp   = Get-Date -Format 'yyyy-MM-dd_HHmm'
    $zipPath = Join-Path $ZipRoot "TargCCOrders_$stamp.zip"

    Step "Creating $zipPath"
    if (Test-Path $zipPath) { Remove-Item $zipPath -Force }
    Compress-Archive -Path (Join-Path $StageRoot '*') -DestinationPath $zipPath

    $sizeMb = [math]::Round((Get-Item $zipPath).Length / 1MB, 1)
    Write-Host ''
    Write-Host "Package ready: $zipPath ($sizeMb MB)" -ForegroundColor Green
}
else {
    Write-Host ''
    Write-Host "Package ready: $StageRoot" -ForegroundColor Green
}

Write-Host ''
Write-Host 'On the server:' -ForegroundColor Yellow
Write-Host '  1. Stop-WebAppPool -Name ''Orders.target.co.il''   (elevated PowerShell)'
Write-Host '  2. Extract the archive over D:\Webs\Orders.target.co.il'
Write-Host '  3. Start-WebAppPool -Name ''Orders.target.co.il'''
Write-Host '  4. curl.exe -i -k https://orders.target.co.il/api/health'
Write-Host ''
Write-Host 'The archive holds no configuration files, so the site keeps its own'
Write-Host 'connection string and LogLocation. Extracting over the folder is safe.'
