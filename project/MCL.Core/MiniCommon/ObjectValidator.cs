using System;
using System.Collections.Generic;
using System.Linq;
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
}
