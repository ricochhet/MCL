using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCL.Core.Launcher.Services;
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon;

public class ObjectValidator<T>
{
    private readonly List<ValidationRule<T>> _rules;

    public ObjectValidator() => _rules = [];

    /// <summary>
    /// Add a new validation rule.
    /// </summary>
    public void AddRule(Func<T, bool> rule, string errorMessage) =>
        _rules.Add(new ValidationRule<T>(rule, errorMessage));

    /// <summary>
    /// Validate object of type T.
    /// </summary>
    public bool Validate(T obj, out List<string> errors)
    {
        errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        return errors.Count == 0;
    }

    /// <summary>
    /// Validate object of type T.
    /// </summary>
    public bool Validate(T obj, Action<List<string>> action)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        action(_errors);
        return _errors.Count == 0;
    }

    /// <summary>
    /// Validate object of type T, and automatically output errors.
    /// </summary>
    public bool Validate(T obj)
    {
        List<string> _errors = _rules.Where(rule => !rule.Rule(obj)).Select(rule => rule.ErrorMessage).ToList();
        foreach (string error in _errors)
            NotificationService.Error(error);
        return _errors.Count == 0;
    }

    /// <summary>
    /// Validate an array of strings is not null, empty, or whitespace.
    /// </summary>
    public static bool IsNotNullOrWhiteSpace(
        string[] properties,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrWhiteSpace(properties, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate an array of strings is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(
        string[] properties,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<T> validator = new();

        foreach (string property in properties ?? [])
            validator.AddRule(
                a => !string.IsNullOrWhiteSpace(property),
                $"Property cannot be null, empty, or whitespace.\nMember: {memberName}\nSource: {sourceFilePath}\nLine: {sourceLineNumber}"
            );

        return !validator.Validate(default);
    }

    /// <summary>
    /// Validate a list is not null, or empty.
    /// </summary>
    public static bool IsNotNullOrEmpty(
        List<T> obj,
        List<T>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrEmpty(obj, properties, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate a dictionary is not null, or empty.
    /// </summary>
    public static bool IsNotNullOrEmpty<T1, T2>(
        Dictionary<T1, T2> obj,
        Dictionary<T1, T2>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNullOrEmpty(obj, properties, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate a list is null, or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T1>(
        List<T1> obj,
        List<T1>[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    )
    {
        ObjectValidator<List<T1>> validator = new();

        string message =
            $"Property cannot be null or empty.\nMember: {memberName}\nSource: {sourceFilePath}\nLine: {sourceLineNumber}";
        validator.AddRule(a => obj != null && obj.Count > 0, message);
        foreach (List<T1> property in properties ?? [])
            validator.AddRule(a => property != null && property.Count > 0, message);

        return !validator.Validate(obj);
    }

    /// <summary>
    /// Validate a dictionary is null, or empty.
    /// </summary>
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
        validator.AddRule(a => obj != null && obj.Count > 0, message);
        foreach (Dictionary<T1, T2> property in properties ?? [])
            validator.AddRule(a => property != null && property.Count > 0, message);

        return !validator.Validate(obj);
    }

    /// <summary>
    /// Validate object of type T is not null.
    /// </summary>
    public static bool IsNotNull(
        T obj,
        object[] properties = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => !IsNull(obj, properties, memberName, sourceFilePath, sourceLineNumber);

    /// <summary>
    /// Validate object of type T is null.
    /// </summary>
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
        foreach (object property in properties ?? [])
            validator.AddRule(a => property != null, message);

        return !validator.Validate(obj);
    }
}
