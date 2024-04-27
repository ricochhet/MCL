dotnet clean
dotnet restore
dotnet publish project\MCL.Launcher\MCL.Launcher.csproj -c Release -r win-x64 --output bin/win-x64 --self-contained -p:TieredCompilation=true -p:PublishReadyToRun=true -p:PublishSingleFile=true