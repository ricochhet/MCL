$ScriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path

$RoslynatorNupkg = "https://globalcdn.nuget.org/packages/roslynator.analyzers.4.12.2.nupkg"
$DownloadPath = Join-Path -Path $ScriptDirectory -ChildPath "../.nupkg/"
$ExtractPath = Join-Path -Path $ScriptDirectory -ChildPath "../.nupkg/roslynator"

if (-not (Test-Path -Path $DownloadPath)) {
    New-Item -Path $DownloadPath -ItemType Directory | Out-Null
}

if (-not (Test-Path -Path $ExtractPath)) {
    New-Item -Path $ExtractPath -ItemType Directory | Out-Null
}

Invoke-WebRequest -Uri $RoslynatorNupkg -OutFile "$DownloadPath\roslynator.analyzers.4.12.2.nupkg"
Expand-Archive -Path "$DownloadPath\roslynator.analyzers.4.12.2.nupkg" -DestinationPath $ExtractPath -Force
