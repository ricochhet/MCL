using System;
using System.Collections.Generic;
using System.Diagnostics;
using MCL.Core.Logger.Enums;
using MCL.Core.Services.Launcher;

namespace MCL.Core.Helpers;

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
            };

        if (environmentalVariables != null && environmentalVariables.Count > 0)
        {
            foreach ((string name, string item) in environmentalVariables)
            {
                startInfo.EnvironmentVariables[name] = item;
            }
        }

        try
        {
            Process process = new() { StartInfo = startInfo };

            if (!useShellExecute)
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                    {
                        NotificationService.Log(NativeLogLevel.Info, "log", [e.Data]);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
            }
            else
            {
                process.Start();
                NotificationService.Log(NativeLogLevel.Info, "log", [process.StandardOutput.ReadToEnd()]);
            }

            process.WaitForExit();
        }
        catch (Exception ex)
        {
            NotificationService.Log(
                NativeLogLevel.Error,
                "log.stack.trace",
                [ex.Message, ex.StackTrace ?? LocalizationService.Translate("stack.trace.null")]
            );
        }
    }
}
