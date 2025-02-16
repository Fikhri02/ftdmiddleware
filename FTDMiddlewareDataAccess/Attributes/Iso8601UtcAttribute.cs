using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FTDMiddlewareDataAccess.Attributes;

public class Iso8601UtcAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string timestamp)
        {
            return DateTime.TryParseExact(
                timestamp,
                "yyyy-MM-ddTHH:mm:ss.fffffffZ",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out _
            );
        }
        return false;
    }
}
