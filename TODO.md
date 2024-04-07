## TODO
TODO tasks are in order from top-to-bottom, most-to-least important.

Tasks marked with (*) are easier to do.

- [x] Evaluate error handling of objects and null-reference handling.
    - [ ] Add logging to error handles.
- [ ] Implement log information object.
    - This provides more consistent logging and client frontend logging.
- [ ] Evaluate configuration properties and architecture.
    - This includes unifying property names.
- [ ] (*) JVM arguments should be stored.
    - Stored arguments should be categorized by the loader.
    - As a byproduct; user-specific information is stored, e.g. username.
- [ ] Implement DownloadStatistic object.
    - Allows better implementation of logging and ui.
    - DownloadStatistic should include the size of the download if applicable.
    - DownloadStatistic should implement a DowloadStatistics parent.
        - The parent object should store the amount of files to download.
        - The parent object should contain a list of DownloadStatistic objects.
- [ ] Handling of missed file downloads due to an error.
    - We should restart the download process if the download files.
        - The download process should not continue trying to download files after the first fail.
        - Continuous failures can indicate a connection problem and cause instability within the program.
        - The download process should indicate failures, and be given a number of chances to continue.
            - After the chances have been used, the user will have to manually continue downloads.
- [ ] Implement downloading of Fabric/Forge/Quilt loaders.
    - [x] Fabric
    - [ ] (*) Quilt
    - [ ] Forge
- [ ] Automate installation of Fabric/Forge/Quilt loaders.
    - [ ] (*) Fabric
    - [ ] Quilt
    - [ ] Forge
- [ ] Implement frontend interface via Avalonia.
- [ ] Add support for downloading and installing third-party server clients.
    - This includes the functionality to run the clients.
- [ ] Implement player skin options on non-vanilla loaders.
    - Non-vanilla loaders can use modifications such as OfflineSkins to provide skins without in-depth networking systems.
    - Skins can still be supplied via an API or website to reduce the manual processes.
- [x] Better handling of JVM arguments.
    - Every JVM argument should be an object that we can parse.
    - JVM argument objects should be added to a list.
- [x] Pre-download version details when downloading the JRE so the user does not have to specify the runtime type.
    - This should be an optional step. The user should be able to have full control over the downloads.
- [x] Implement the usage of the downloaded Java runtime.
    - This includes the setting of environment variables, e.g. JAVA_HOME.