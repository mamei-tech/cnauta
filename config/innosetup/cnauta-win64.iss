; -- 64Bit.iss --
; Demonstrates installation of a program built for the x64 (a.k.a. AMD64)
; architecture.
; To successfully run this installation and the program it installs,
; you must have a "x64" edition of Windows.

; SEE THE DOCUMENTATION FOR DETAILS ON CREATING .ISS SCRIPT FILES!

[Setup]
AppName=cnauta
AppVersion=0.3.0
VersionInfoVersion=0.3.0
VersionInfoDescription=CNauta App
WizardStyle=modern
DefaultDirName={autopf}\mamei\cnauta
DefaultGroupName=cnauta
UninstallDisplayIcon={app}\cnauta.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:CNauta
OutputBaseFilename=cnauta-0.3.0-x64-setup
; "ArchitecturesAllowed=x64" specifies that Setup cannot run on
; anything but x64.
ArchitecturesAllowed=x64
; "ArchitecturesInstallIn64BitMode=x64" requests that the install be
; done in "64-bit mode" on x64, meaning it should use the native
; 64-bit Program Files directory and the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64
LicenseFile=license.txt
PrivilegesRequiredOverridesAllowed=dialog

[Files]
Source: "cnauta.exe"; DestDir: "{app}"; DestName: "cnauta.exe"
Source: "HtmlAgilityPack.dll"; DestDir: "{app}"
Source: "Newtonsoft.Json.dll"; DestDir: "{app}"
Source: "appconfig.json"; DestDir: "{app}"; Permissions: users-full

[Dirs]
Name: {app}; Permissions: users-full

[Icons]
Name: "{group}\cnauta"; Filename: "{app}\cnauta.exe"
