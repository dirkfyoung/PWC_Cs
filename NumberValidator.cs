using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWC_Cs
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string Message { get; }

        public ValidationResult(bool isValid, string message = "")
        {
            IsValid = isValid;
            Message = message;
        }
    }

    public static class NumberValidator
    {
        public static ValidationResult TestRealNumbers(string input, string? exception = null)
        {
            if (input == exception)
                return new ValidationResult(true);

            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
                return new ValidationResult(false, $"Invalid real number: {input}");

            if (input.Contains(","))
                return new ValidationResult(false, $"No commas allowed: {input}");

            return new ValidationResult(true);
        }

        public static ValidationResult TestActualIntegers(string input)
        {
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out _))
                return new ValidationResult(false, $"Value should be an integer: {input}");

            if (input.Contains(","))
                return new ValidationResult(false, $"No commas allowed: {input}");

            return new ValidationResult(true);
        }
    }
}