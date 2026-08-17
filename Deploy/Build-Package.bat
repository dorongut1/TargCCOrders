@echo off
REM Build-Package.bat - double-click launcher for Build-Package.ps1.
REM Arguments are passed straight through, e.g. Build-Package.bat -NoZip
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Build-Package.ps1" %*
echo.
pause
