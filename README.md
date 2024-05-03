## MCL
A CLI based Minecraft launcher with support for Fabric and Quilt mod loaders.

> [!Warning]
> #### Under Development
> ###### There is no stable version.

## Privacy
MCL is an open source project. Your commit credentials as author of a commit will be visible by anyone. Please make sure you understand this before submitting a PR.
Feel free to use a "fake" username and email on your commits by using the following commands:
```bash
git config --local user.name "USERNAME"
git config --local user.email "USERNAME@SOMETHING.com"
```

## Requirements
- .NET 8 SDK or higher.
- Visual Studio Code or alternatives.
- [NetCoreDbg](https://github.com/Samsung/netcoredbg) or alternatives.

## Build
1. Install Cake: `dotnet tool install Cake.Tool --version 4.0.0` or run `dotnet tool restore` (from `/project/` root).
2. Run `dotnet-cake build.cake` (from `/project/` root).
3. Output will be located in `project/Build/*`.

If you want to build with an icon, you can place an icon in `{root}/project/MCL.Resources.Local/icon.ico`.

Files placed in `{root}/project/MCL.Resources` or `{root}/project/MCL.Resources.Local` will be copied over to `project/Build`.

## Development
1. Install Roslynator Analyzers: `./scripts/setup.ps1` (from `/MCL/` root).
    - [SonarLint](https://www.sonarsource.com/products/sonarlint/) is used for development, it is optional, but recommended.
2. Use or reference `.vscode/tasks.json` for formatting, code analysis, and building.
3. Follow [build steps](#build) for building.

## Platforms

|        | Windows|Linux (Untested)|Mac OS (Untested)|
|--------|--------|----------------|-----------------|
| x86-64 | ✅ | ❌ | ❌ |
| x86    | ❌ | ❌ | ❌ |
| ARM64  | ❌ | ❌ | ❌ |

## Mod Loaders
(*) Denotes unsupported mod loaders.

|        | Windows|Linux (Untested)|Mac OS (Untested)|
|--------|--------|----------------|-----------------|
| Fabric | ✅ | ❌ | ❌ |
| Quilt  | ❌ | ❌ | ❌ |
| *Forge | ❌ | ❌ | ❌ |

## Localization (Translations)
All non-machine translation contributions for any languages are welcome.
- English [View](./project/MCL.Resources/.mcl/localization/localization.en.json)

### Suggestions & PRs
Suggestions and pull requests are welcome, just keep in mind to follow the project architecture to keep it consistent.

### License
See LICENSE file.