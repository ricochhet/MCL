namespace MCL.Core.MiniCommon.Validation.Extensions;

public static class ValidationExt
{
    public static string? NullIfEmpty(this string s) => string.IsNullOrEmpty(s) ? null : s;

    public static string? NullIfWhitespace(this string s) => string.IsNullOrWhiteSpace(s) ? null : s;
}
