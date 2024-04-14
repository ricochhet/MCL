using System;

namespace MCL.Core.Helpers;

public static class StringHelper
{
    public static string Search(string[] lines, string start, char end, int index)
    {
        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (trimmedLine.StartsWith(start))
            {
                string[] parts = trimmedLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return parts[index].TrimEnd(end);
            }
        }

        return string.Empty;
    }
}
