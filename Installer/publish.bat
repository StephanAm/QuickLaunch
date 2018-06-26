@setlocal
@set msBuildPath=C:\Windows\Microsoft.NET\Framework\v4.0.30319
@set projectPath=..\QuickLaunch\QuickLaunch.csproj
@set nsisPath=C:\Program Files (x86)\NSIS
@set path=%path%;%msBuildPath%;%nsisPath%
@set installerName="MyInstaller.exe"
msbuild.exe %projectPath% /p:Configuration=Release
makensis.exe main.nsi
@endlocal