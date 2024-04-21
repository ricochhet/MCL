using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCL.Core.Launcher.Services;
using MCL.Core.Logger.Enums;
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon;

public class ObjectValidator<T>
{
    private readonly List<ValidationRule<T>> _rules;

    public ObjectValidator()
    {
        _rules = [];
    }

    public void AddRule(Func<T, bool> rule, string errorMessage)
    {
        _rules.Add(new ValidationRule<T>(rule, errorMessage));
    }

    public bool Validate(T obj, out List<string> errors)
    {
        errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        return errors.Count == 0;
    }

    public bool Validate(T obj, Action<List<string>> action)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        action(_errors);
        return _errors.Count == 0;
    }

    public bool Validate(T obj)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        foreach (string error in _errors)
            NotificationService.Log(NativeLogLevel.Error, "log", error);
        return _errors.Count == 0;
    }

    public static bool IsNotNullOrWhiteSpace(
        string[] properties,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        return !IsNullOrWhiteSpace(properties, memberName, sourceFilePath, sourceLineNumber);
    }

    public static bool IsNullOrWhiteSpace(
        string[] properties,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<T> validator = new();

        foreach (string property in properties)
            validator.AddRule(
                a => !string.IsNullOrWhiteSpace(property),
                $"Property cannot be null, empty, or whitespace.\nMember: {memberName}\nSource: {sourceFilePath}\nLine: {sourceLineNumber}"
            );

        return !validator.Validate(default);
    }

    public static bool IsNotNullOrEmpty(
        List<T> obj,
        List<T>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        return !IsNullOrEmpty(obj, properties, memberName, sourceFilePath, sourceLineNumber);
    }

    public static bool IsNotNullOrEmpty<T1, T2>(
        Dictionary<T1, T2> obj,
        Dictionary<T1, T2>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        return !IsNullOrEmpty(obj, properties, memberName, sourceFilePath, sourceLineNumber);
    }

    public static bool IsNullOrEmpty(
        List<T> obj,
        List<T>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<T> validator = new();

        string message =
            $"Property cannot be null or empty.\nMember: {memberName}\nSource: {sourceFilePath}\nLine: {sourceLineNumber}";
        validator.AddRule(a => obj != null, message);
        foreach (List<T> property in properties)
            validator.AddRule(a => property != null && property.Count > 0, message);

        return !validator.Validate(default);
    }

    public static bool IsNullOrEmpty<T1, T2>(
        Dictionary<T1, T2> obj,
        Dictionary<T1, T2>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<Dictionary<T1, T2>> validator = new();

        string message =
            $"Property cannot be null or empty.\nMember: {memberName}\nSource: {sourceFilePath}\nLine: {sourceLineNumber}";
        validator.AddRule(a => obj != null, message);
        foreach (Dictionary<T1, T2> property in properties)
            validator.AddRule(a => property != null && property.Count > 0, message);

        return !validator.Validate(default);
    }

    public static bool IsNotNull(
        T obj,
        object[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        return !IsNull(obj, properties, memberName, sourceFilePath, sourceLineNumber);
    }

    public static bool IsNull(
        T obj,
        object[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<T> validator = new();

        string message =
            $"Property cannot be null or empty.\nMember: {memberName}\nSource: {sourceFilePath}\nLine: {sourceLineNumber}";
        validator.AddRule(a => obj != null, message);
        foreach (object property in properties)
            validator.AddRule(a => property != null, message);

        return !validator.Validate(obj);
    }
}
