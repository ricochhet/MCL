using System;
using System.Collections.Generic;
using System.Linq;
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

    public static bool IsNullOrWhitespace(params string[] properties)
    {
        ObjectValidator<T> validator = new();

        foreach (string property in properties)
            validator.AddRule(
                a => !string.IsNullOrWhiteSpace(property),
                $"Property cannot be null, empty, or whitespace."
            );

        return !validator.Validate(default);
    }

    public static bool IsNullOrEmpty(T obj, params List<T>[] properties)
    {
        ObjectValidator<T> validator = new();

        foreach (List<T> property in properties)
            validator.AddRule(a => property != null && property.Count > 0, $"Property cannot be null or empty.");

        return !validator.Validate(obj);
    }

    public static bool IsNull(T obj, params object[] properties)
    {
        ObjectValidator<T> validator = new();

        foreach (object property in properties)
            validator.AddRule(a => property != null, $"Property cannot be null or empty.");

        return !validator.Validate(obj);
    }

    public static ObjectValidator<T> Validator(params string[] properties)
    {
        ObjectValidator<T> validator = new();

        foreach (string property in properties)
            validator.AddRule(a => !string.IsNullOrWhiteSpace(property), $"Property cannot be null or empty.");

        return validator;
    }

    public static ObjectValidator<T> Validator(params object[] properties)
    {
        ObjectValidator<T> validator = new();

        foreach (object property in properties)
            validator.AddRule(a => property != null, $"Property cannot be null.");

        return validator;
    }
}
