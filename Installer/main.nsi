# declare name of installer file
Outfile $%installerName%
InstallDir "$PROGRAMFILES\QuickLaunch"

# open section
Section
    SetOutPath $InstDir
    File /r "..\QuickLaunch\bin\Release\*"
    File "icon.ico"
    # Show Success message.
    MessageBox MB_OK "Installed to $InstDir"
    WriteUninstaller $InstDir\uninstall.exe
    createDirectory "$SMPROGRAMS\QuickLaunch"
    createShortcut "$SMPROGRAMS\QuickLaunch\QuickLaunch.lnk" "$InstDir\QuickLaunch.exe" "" "$InstDir\icon.ico"
    createShortcut "$SMPROGRAMS\QuickLaunch\uninstall.lnk" "$InstDir\uninstall.exe" "" ""
    WriteRegStr HKLM "Software\QuickLaunch" "InstallDir" "$InstDir"
# end the section
SectionEnd

Section "Uninstall"
    RMDir /r $InstDir
    RMDir /r "$SMPROGRAMS\QuickLaunch"

SectionEnd