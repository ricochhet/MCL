param(
    [string]$config = "Release"
)

if ($config -eq "Release" -or $config -eq "Debug") {
    dotnet csharpier .
    dotnet-cake build.cake --config=$config
} else {
    Write-Host "Invalid configuration: $config"
}