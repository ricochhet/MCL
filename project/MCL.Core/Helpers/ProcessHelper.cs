using System;
using System.Diagnostics;
using MCL.Core.Logger;

namespace MCL.Core.Helpers;

public static class ProcessHelper
{
    public static void RunProcess(string fileName, string arguments, string workingDirectory, bool useShellExecute = true)
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
                        UseShellExecute = useShellExecute
                    },
                };

            process.StartInfo.RedirectStandardOutput = !useShellExecute;
            process.Start();
            LogBase.Info(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
        }
        catch (Exception e)
        {
            LogBase.Error(e.ToString());
        }
    }
}
