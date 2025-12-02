using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PWC_Cs
{
    public class NumberValidator
    {
        public static void TestRealNumbers(ref bool isValid, ref string message, TextBox textBox)
        {
            InitializeValidation(ref isValid, textBox);

            if (!double.TryParse(textBox.Text, out _))
            {
                SetErrorState(ref isValid, ref message, textBox, "Check the value for");
                return;
            }

            CheckForCommas(ref isValid, ref message, textBox);
        }

        public static void TestRealNumbers(ref bool isValid, ref string message, TextBox textBox, string exception)
        {
            InitializeValidation(ref isValid, textBox);

            if (textBox.Text == exception)
            {
                return;
            }

            if (!double.TryParse(textBox.Text, out _))
            {
                SetErrorState(ref isValid, ref message, textBox, "Check the value for");
                return;
            }

            CheckForCommas(ref isValid, ref message, textBox);
        }

        public static void TestActualIntegers(ref bool isValid, ref string message, string input)
        {
            isValid = true;

            if (!int.TryParse(input, out int parsedNumber))
            {
                message = "Value should be an integer";
                isValid = false;
                return;
            }

            if (double.TryParse(input, out double parsedDouble) && Math.Abs(parsedDouble - parsedNumber) > 0.01)
            {
                message = "Value should be an integer";
                isValid = false;
                return;
            }

            if (input.Contains(","))
            {
                message = $"No commas allowed for {input}";
                isValid = false;
            }
        }

        private static void InitializeValidation(ref bool isValid, TextBox textBox)
        {
            isValid = true;
            textBox.BackColor = Color.White;
        }

        private static void CheckForCommas(ref bool isValid, ref string message, TextBox textBox)
        {
            if (textBox.Text.Contains(","))
            {
                SetErrorState(ref isValid, ref message, textBox, "No commas allowed for");
            }
        }

        private static void SetErrorState(ref bool isValid, ref string message, TextBox textBox, string errorMessage)
        {
            textBox.BackColor = Color.Orange;
            message = $"{errorMessage} {textBox.Name}";
            isValid = false;
        }
    }
}