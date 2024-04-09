using System;
using System.Collections.Generic;
using System.Diagnostics;
using MCL.Core.Logger;

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
                        LogBase.Info(e.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
            }
            else
            {
                process.Start();
                LogBase.Info(process.StandardOutput.ReadToEnd());
            }

            process.WaitForExit();
        }
        catch (Exception e)
        {
            LogBase.Error(e.ToString());
        }
    }
}
