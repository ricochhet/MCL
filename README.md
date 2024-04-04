## MCL
Minecraft Launcher

## Privacy
MCL is an open source project. Your commit credentials as author of a commit will be visible by anyone. Please make sure you understand this before submitting a PR.
Feel free to use a "fake" username and email on your commits by using the following commands:
```bash
git config --local user.name "USERNAME"
git config --local user.email "USERNAME@SOMETHING.com"
```

## Requirements
- .NET 8 SDK
- Visual Studio Code

## Build
The primary way to build is using [Cake](https://cakebuild.net/).

1. Install Cake: `dotnet tool install Cake.Tool --version 3.2.0` or run `dotnet tool restore` (from `/project/` root).
2. Run `dotnet-cake build.cake` (from `/project/` root).
3. Output will be located in `project/Build/*`.

If you want to build with an icon, you can place an icon in `{root}/bin/Assets/icon.ico`.

Files placed in `{root}/bin/MCL` will be copied over to `project/Build`.