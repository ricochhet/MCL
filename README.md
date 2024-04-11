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

Files placed in `{root}/project/MCL.Resources` will be copied over to `project/Build`.

### Developers
If you intend to publish a build, use the above guide.

1. Install tools via `dotnet tool restore`.
    - If you make any changes to the code use `dotnet csharpier .` in the project root to format.
2. Run `install.ps1 -build <project> -config <config>`
    - `<project>` options:
        - `launcher`
    - `<config>` options:
        - `Release` | `Release-Publish`
        - `Debug` | `Release-Debug`
3. Output will be located in `project/MCL.Launcher/bin/{Release/Debug/publish}/net8.0/win-x64`

### Suggestions & PRs
Suggestions and pull requests are welcome, just keep in mind to follow the project architecture to keep it consistent.

### License
See LICENSE file.