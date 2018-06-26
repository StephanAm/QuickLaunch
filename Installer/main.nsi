# declare name of installer file
Outfile $%installerName%
InstallDir "$PROGRAMFILES\QuickLaunch"

# open section
Section
    SetOutPath $InstDir
    File /r "..\QuickLaunch\bin\Release\*"

    # Show Success message.
    MessageBox MB_OK "Installed to $InstDir"
    WriteUninstaller $InstDir\uninstall.exe
 
# end the section
SectionEnd

Section "Uninstall"
    RMDir /r $InstDir
SectionEnd