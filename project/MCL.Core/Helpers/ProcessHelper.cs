using System;
using System.Diagnostics;
using MCL.Core.Logger;

namespace MCL.Core.Helpers;

public static class ProcessHelper
{
    public static void RunProcess(
        string fileName,
        string arguments,
        string workingDirectory,
        bool useShellExecute = true
    )
    {
        try
        {
            Process process =
                new()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = arguments,
                        WorkingDirectory = workingDirectory,
                        UseShellExecute = useShellExecute,
                        RedirectStandardOutput = !useShellExecute,
                    },
                };

            if (!useShellExecute)
            {
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
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
