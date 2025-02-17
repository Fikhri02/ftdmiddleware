using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using FTDMiddlewareDataAccess.Models.RequestModels;

public class SignatureValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is not string providedSignature)
            return new ValidationResult("sig is required.");

        var request = (SubmitTrxMessageRequest)validationContext.ObjectInstance;

        string formattedTimestamp = request.Timestamp.ToString("yyyyMMddHHmmss");

        string rawString = $"{formattedTimestamp}{request.PartnerKey}{request.PartnerRefNo}{request.TotalAmount}{request.PartnerPassword}";

        string expectedSignature = ComputeSha256Base64(rawString);

        if (providedSignature != expectedSignature)
        {
            return new ValidationResult("Access Denied!");
        }

        return ValidationResult.Success;
    }

    private static string ComputeSha256Base64(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hashBytes);
    }
}
