# declare name of installer file
Outfile "hello world.exe"
InstallDir "$PROGRAMFILES\QuickLaunch"
# open section
Section
    SetOutPath $InstDir
    File "readme.txt"
    File /r ".\Source\*"

    # Show Success message.
    MessageBox MB_OK "Installed to $InstDir"
 
 
# end the section
SectionEnd