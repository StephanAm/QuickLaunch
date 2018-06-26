# declare name of installer file
Outfile "hello world.exe"
InstallDir "$PROGRAMFILES\QuickLaunch"
# open section
Section
    SetOutPath $InstDir
    File /r "..\QuickLaunch\bin\Release\*"

    # Show Success message.
    MessageBox MB_OK "Installed to $InstDir"
 
 
# end the section
SectionEnd