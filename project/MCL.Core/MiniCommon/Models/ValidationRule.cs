using System;

namespace MCL.Core.MiniCommon.Models;

public class ValidationRule<T>(Func<T, bool> rule, string errorMessage)
{
    public Func<T, bool> Rule { get; } = rule;
    public string ErrorMessage { get; } = errorMessage;
}
