## TODO
TODO tasks are in order from top-to-bottom, most-to-least important.

Tasks marked with (*) are easier to do.

- [ ] Implement a frontend user interface. See: [Todo (UI)](#todo-ui)
- [ ] Implement player skin options on non-vanilla loaders.
    - Non-vanilla loaders can use modifications such as OfflineSkins to provide skins without in-depth networking systems.
    - Skins can still be supplied via an API or website to reduce the manual processes.
- [ ] Consider implementation of local API builder to self-host files and data for offline usage.
- [x] Add message overriding to object validation for simplified user messaging flow.
    - [x] Add message overriding to validation shims.
- [x] Implement configuration migration service.
    - This is beneficial when the configuration model is refactored.
- [x] Implement object validator throughout project.
- [x] Download commands should require a version flag.
- [x] Implement instance data to assist in managing game and mod loader versions.
- [x] Refactor file naming conventions for downloaded JSON data. 
- [x] Implement MCL.CodeAnalyzers
    - This tool will analyze the project for project-specific rules and warn when the rule is not followed.
    - [x] Rule: Namespaces should match physical path, e.g. MCL.Core.Models == MCL/Core/Models/*
    - [x] Rule: Localization keys must exist.
- [x] Code cleanup 1
    - [x] Cleanup calls to path resolvers.
    - [x] Cleanup variable names for consistency.
    - [x] Cleanup LaunchHelper.
    - [x] Add documentation.
    - [x] Update class path helper to use user-specified platform.
- [x] Implement UNZIP_AND_COPY file deployment.
    - [x] Implement download and extraction of 7z for use.
    - [x] Implement 7z arguments for processhelper.
- [x] Evaluate for unnecessary constructors.
- [x] Refactor code to utilize VFS common library with applicable functions.
- [x] Evaluate error handling of objects and null-reference handling.
    - [x] Add logging to error handles.
- [x] Implement log information object.
    - This provides more consistent logging and client frontend logging.
- [x] Evaluate configuration properties and architecture.
    - This includes unifying property names.
- [x] JVM arguments should be stored.
    - Stored arguments should be categorized by the loader.
    - As a byproduct; user-specific information is stored, e.g. username.
- [x] ~~Implement DownloadStatistic object.~~ Implemented as RequestData.
    - Allows better implementation of logging and ui.
    - DownloadStatistic should include the size of the download if applicable.
    - DownloadStatistic should implement a DowloadStatistics parent.
        - The parent object should store the amount of files to download.
        - The parent object should contain a list of DownloadStatistic objects.
- [x] Handling of missed file downloads due to an error.
    - We should restart the download process if the download files.
        - The download process should not continue trying to download files after the first fail.
        - Continuous failures can indicate a connection problem and cause instability within the program.
        - The download process should indicate failures, and be given a number of chances to continue.
            - After the chances have been used, the user will have to manually continue downloads.
- [x] Implement downloading of Fabric/Forge/Quilt loaders.
    - [x] Fabric
    - [x] Quilt
    - ~~Forge~~ Won't Implement
- [x] Automate installation of Fabric/Forge/Quilt loaders.
    - [x] Fabric
        - [x] Implement installation of Fabric via installer.
    - [x]  Quilt
    - ~~Forge~~ Won't Implement
- [x] Implement simple management of Fabric / Quilt mods.
- [x] Add support for downloading and installing third-party server clients.
    - This includes the functionality to run the clients.
- [x] Better handling of JVM arguments.
    - Every JVM argument should be an object that we can parse.
    - JVM argument objects should be added to a list.
- [x] Pre-download version details when downloading the JRE so the user does not have to specify the runtime type.
    - This should be an optional step. The user should be able to have full control over the downloads.
- [x] Implement the usage of the downloaded Java runtime.
    - This includes the setting of environment variables, e.g. JAVA_HOME.

## Todo (UI)
View the options considered for user interface below.

## Specifications
- Game version dropdown menu.
- Mod loader version dropdown menu(s).
- Download & Run buttons.
- Username input field.
- Log view window.

#### Avalonia
- Cross-platform.
- Supports Native AOT.
    - Does not support single file. Produces additional native DLLs. `libHarfBuzzSharp.dll` and `libSkaSharp.dll`
- Similar experience to WPF.

#### Terminal.Gui
- Cross-platform.
- Doesn't support Native AOT.
    - Supposedly planned for later release.
- Quick iteration, performant.

#### ImGui.NET
- Cross-platform.
- Supports Native AOT.
    - It is a wrapper around a native library.
- Can be cumbersome to work with.
    - Not necessarily an issue for simple applications.

#### WinFormsComInterop 
[view](https://github.com/kant2002/WinFormsComInterop)
- No cross-platform support.
- Supports Native AOT for WinForms.
    - WPF support exists, currently broken.
- Supports WPF ecosystem.