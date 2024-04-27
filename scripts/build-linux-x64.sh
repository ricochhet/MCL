#!/usr/bin/sh

dotnet clean
dotnet restore
dotnet publish project/MCL.Launcher/MCL.Launcher.csproj -c Release -r linux-x64 --output bin/linux-x64 --self-contained -p:PublishReadyToRun=true -p:PublishSingleFile=true