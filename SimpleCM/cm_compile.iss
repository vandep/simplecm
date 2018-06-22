[Setup]
AppName=MiniCM
AppVersion=1.0
DefaultDirName={pf}\MiniCM
DefaultGroupName=MiniCM
UninstallDisplayIcon={app}\favicon.ico
Compression=lzma2
SolidCompression=yes
OutputDir=\
OutputBaseFilename=MiniCMSetup
[Files]
Source: "bin\Release\SimpleCM.exe"; DestDir: "{app}";Flags: ignoreversion replacesameversion;
Source: "bin\Release\SimpleCM.exe.config"; DestDir: "{app}";Flags: ignoreversion replacesameversion;
Source: "bin\Release\SimpleCM.pdb"; DestDir: "{app}";Flags: ignoreversion replacesameversion;
Source: "bin\Release\System.Data.SQLite.DLL"; DestDir: "{app}";Flags: ignoreversion replacesameversion;
Source: "bin\Release\favicon.ico"; DestDir: "{app}";Flags: ignoreversion replacesameversion;

[icons]
;桌面
Name: "{commondesktop}\MiniCM" ;Filename: "{app}\SimpleCM.exe"; WorkingDir: "{app}" ; IconFilename: "{app}\favicon.ico"

;开始->程序
Name: "{commonprograms}\MiniCM" ;Filename: "{app}\SimpleCM.exe"; WorkingDir: "{app}"; IconFilename: "{app}\favicon.ico"
Name: "{commonprograms}\MiniCM" ;Filename: "{app}\SimpleCM.exe"; WorkingDir: "{app}" ;IconFilename: "{app}\favicon.ico"

;快捷启动
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\MiniCM"; Filename: "{app}\SimpleCM.exe"; WorkingDir: {app}; IconFilename: "{app}\favicon.ico";
