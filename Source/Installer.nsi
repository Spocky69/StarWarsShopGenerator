; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects.
;
; See install-shared.nsi for a more robust way of checking for administrator rights.
; See install-per-user.nsi for a file association example.

;--------------------------------

; The name of the installer
Name "StarWarsShopGeneratorInstaller"

; The file to write
OutFile "StarWarsShopGeneratorInstaller.exe"

; Request application privileges for Windows Vista and higher
RequestExecutionLevel admin

; Build Unicode installer
Unicode True

; The default installation directory
InstallDir $PROGRAMFILES\StarWarsShopGenerator

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\StarWarsShopGenerator" "Install_Dir"

;--------------------------------

; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section "Example2 (required)"

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File ShopGenerator\bin\Release\StarWarsShopGenerator.exe
  File ShopGenerator\bin\Release\StarWarsShopGenerator.pdb
  File ShopGenerator\bin\Release\StarWarsShopGenerator.exe.config
  File ShopGenerator\bin\Release\StarWarsShopGenerator.exe.manifest
  File /r *.csv
  File /r *.loc
  
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\StarWarsShopGenerator "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Example2" "DisplayName" "StarWarsShopGenerator"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Example2" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Example2" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Example2" "NoRepair" 1
  WriteUninstaller "$INSTDIR\uninstall.exe"
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\StarWarsShopGenerator"
  CreateShortcut "$SMPROGRAMS\StarWarsShopGenerator\Uninstall.lnk" "$INSTDIR\uninstall.exe"
  CreateShortcut "$SMPROGRAMS\StarWarsShopGenerator\StarWarsShopGenerator (MakeNSISW).lnk" "$INSTDIR\ShopGenerator.exe"

SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\StarWarsShopGenerator"
  DeleteRegKey HKLM SOFTWARE\StarWarsShopGenerator

  ; Remove files and uninstaller
  Delete $INSTDIR\*.*
  RMDir /REBOOTOK $INSTDIR\Database
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\StarWarsShopGenerator\*.lnk"

  ; Remove directories
  RMDir /REBOOTOK "$SMPROGRAMS\StarWarsShopGenerator"
  RMDir "$INSTDIR"

SectionEnd
