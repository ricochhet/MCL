using System.Collections.Generic;
using System.Linq;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;

namespace MCL.Core.Logger;

public static class Watermark
{
    public static void Draw(List<string> text)
    {
        List<string> result = [];
        int longestLength = text.Max(TextLength);
        string line = new('-', longestLength);
        result.Add($"┌─{line}─┐");

        foreach (string textItem in text)
        {
            int spacingSize = longestLength - TextLength(textItem);
            string spacingText = textItem + new string(' ', spacingSize);
            result.Add($"│ {spacingText} │");
        }

        result.Add($"└─{line}─┘");
        foreach (string textItem in result)
        {
            NotificationService.Log(NativeLogLevel.Info, "log", [textItem]);
        }
    }

    private static int TextLength(string s)
    {
        return s.Replace("[\u0391-\uFFE5]", "ab").Length;
    }
}
