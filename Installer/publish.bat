@setlocal
@set mode=%1
IF /I "%mode%"=="prod" (
    @echo getting master
    git checkout master
    git status
    git pull
)
@set msBuildPath=C:\Windows\Microsoft.NET\Framework\v4.0.30319
@set projectPath=..\QuickLaunch\QuickLaunch.csproj
@set nsisPath=C:\Program Files (x86)\NSIS
@set path=%path%;%msBuildPath%;%nsisPath%
@set installerName="QuickLaunch.exe"
msbuild.exe %projectPath% /p:Configuration=Release
makensis.exe main.nsi
@endlocal