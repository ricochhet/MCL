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
                if (parts.Length >= index + 1)
                    return parts[index].TrimEnd(end);
                return string.Empty;
            }
        }

        return string.Empty;
    }
}
