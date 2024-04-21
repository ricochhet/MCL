using System;
using System.Collections.Generic;
using System.Diagnostics;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;

namespace MCL.Core.MiniCommon.Helpers;

public static class ProcessHelper
{
    public static void RunProcess(
        string fileName,
        string arguments,
        string workingDirectory,
        bool useShellExecute = true,
        Dictionary<string, string> environmentalVariables = null
    )
    {
        ProcessStartInfo startInfo =
            new()
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                UseShellExecute = useShellExecute,
                RedirectStandardOutput = !useShellExecute,
                RedirectStandardError = !useShellExecute,
            };

        if (environmentalVariables != null && environmentalVariables.Count > 0)
        {
            foreach ((string name, string item) in environmentalVariables)
                startInfo.EnvironmentVariables[name] = item;
        }

        try
        {
            Process process = new() { StartInfo = startInfo };

            if (!useShellExecute)
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (ObjectValidator<string>.IsNotNullOrWhiteSpace([e?.Data], NativeLogLevel.Debug))
                        NotificationService.Info(e.Data ?? string.Empty);
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (ObjectValidator<string>.IsNotNullOrWhiteSpace([e?.Data], NativeLogLevel.Debug))
                        NotificationService.Error(e.Data ?? string.Empty);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            else
            {
                process.Start();
                NotificationService.Info(process.StandardOutput.ReadToEnd());
            }

            process.WaitForExit();
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "log.stack.trace",
                ex.Message,
                ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")
            );
        }
    }
}
