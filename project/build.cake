string target = Argument<string>("target", "ExecuteBuild");
string config = Argument<string>("config", "Release");
bool VSBuilt = Argument<bool>("vsbuilt", false);

// Cake API Reference: https://cakebuild.net/dsl/
// setup variables
var buildDir = "./Build";
var csprojPaths = GetFiles("./**/MCL.*(Launcher|CodeAnalyzers|Paper).csproj");
var delPaths = GetDirectories("./*(!MCL.Resources)/*(obj|bin)");
var licenseFile = "../LICENSE";
var publishRuntime = "win-x64";
// var launcherDebugFolders = GetDirectories("./MCL.Resources/*(.mcl|development)");
var launcherDebugFolders = GetDirectories("./*(MCL.Resources|MCL.Resources.Local)");

// Clean build directory and remove obj / bin folder from projects
Task("Clean")
    .WithCriteria(!VSBuilt)
    .Does(() =>
    {
        CleanDirectory(buildDir);
    })
    .DoesForEach(delPaths, (directoryPath) =>
    {
        DeleteDirectory(directoryPath, new DeleteDirectorySettings
        {
            Recursive = true,
            Force = true
        });
    });

// Restore, build, and publish selected csproj files
Task("Publish")
    .IsDependentOn("Clean")
    .DoesForEach(csprojPaths, (csprojFile) => 
    {
        DotNetPublish(csprojFile.FullPath, new DotNetPublishSettings 
        {
            NoLogo = true,
            Configuration = config,
            Runtime = publishRuntime,
            PublishSingleFile = true,
            SelfContained = false,
            OutputDirectory = buildDir
        });
    });

// Copy license to build directory
Task("CopyBuildData")
    .IsDependentOn("Publish")
    .DoesForEach(launcherDebugFolders, (launcherDebugFolder) => 
    {
        if (DirectoryExists(launcherDebugFolder))
        {
            Information(launcherDebugFolder);
            CopyDirectory(launcherDebugFolder, buildDir);
        }
    });

// Remove pdb files from build if running in release configuration
Task("RemovePDBs")
    .WithCriteria(config == "Release")
    .IsDependentOn("CopyBuildData")
    .Does(() => 
    {
        DeleteFiles($"{buildDir}/*.pdb");
    });

Task("CopyLicense")
    .IsDependentOn("RemovePDBs")
    .Does(() => 
    {
        CopyFile(licenseFile, $"{buildDir}/LICENSE.txt");
    });

// Runs all build tasks based on dependency and configuration
Task("ExecuteBuild")
    .IsDependentOn("CopyBuildData")
    .IsDependentOn("CopyLicense");

// Runs target task
RunTarget(target);