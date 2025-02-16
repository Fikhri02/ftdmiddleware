using System;
using System.ComponentModel.DataAnnotations;

public class ValidTimestampAttribute : ValidationAttribute
{
    private readonly int _timeWindowMinutes;

    public ValidTimestampAttribute(int timeWindowMinutes = 5)
    {
        _timeWindowMinutes = timeWindowMinutes;
        ErrorMessage = $"Provided timestamp must be within ±{_timeWindowMinutes} minutes of the server time.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || !(value is string timestampString))
        {
            return new ValidationResult("timestamp is required.");
        }

        if (!DateTime.TryParse(timestampString, out DateTime providedTime))
        {
            return new ValidationResult("Invalid timestamp format. Expected ISO 8601 format.");
        }

        DateTime serverTime = DateTime.UtcNow;
        TimeSpan difference = serverTime - providedTime;

        if (Math.Abs(difference.TotalMinutes) > _timeWindowMinutes)
        {
            return new ValidationResult($"Expired");
        }

        return ValidationResult.Success;
    }
}
