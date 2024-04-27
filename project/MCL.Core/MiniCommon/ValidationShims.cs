using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCL.Core.MiniCommon.Logger.Enums;
using MCL.Core.MiniCommon.Services;

namespace MCL.Core.MiniCommon;

public static class ValidationShims
{
    /// <summary>
    /// Coalescing operator shim for string.Empty to log when it gets called.
    /// </summary>
    public static string StringEmpty(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        NotificationService.Log(
            level,
            "error.validation.string-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return string.Empty;
    }

    /// <summary>
    /// Coalescing operator shim for empty list to log when it gets called.
    /// </summary>
    public static List<T> ListEmpty<T>(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        NotificationService.Log(
            level,
            "error.validation.list-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return [];
    }

    /// <summary>
    /// Coalescing operator shim for empty dictionary to log when it gets called.
    /// </summary>
#pragma warning disable IDE0079
#pragma warning disable S4144
    public static Dictionary<TKey, TValue> DictionaryEmpty<TKey, TValue>(
#pragma warning restore IDE0079, S4144
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where TKey : notnull
    {
        NotificationService.Log(
            level,
            "error.validation.list-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return [];
    }

    /// <summary>
    /// Coalescing operator shim for empty class to log when it gets called.
    /// </summary>
    public static T ClassEmpty<T>(
        NativeLogLevel level = NativeLogLevel.Debug,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
        where T : new()
    {
        NotificationService.Log(
            level,
            "error.validation.class-shim",
            memberName,
            sourceFilePath,
            sourceLineNumber.ToString()
        );
        return new();
    }
}
