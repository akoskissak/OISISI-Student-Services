
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

public class GUIVIewUtils
{
    public static int SafeInputInt(TextBox textBox, Label errorLabel, string prompt, int? defaultValue = null)
    {
        while (true)
        {
            string userInput = textBox.Text;

            if (string.IsNullOrWhiteSpace(userInput) && defaultValue != null)
            {
                return defaultValue.Value;
            }

            if (int.TryParse(userInput, out int result))
            {
                // Reset the error label if the input is valid
                errorLabel.Content = string.Empty;
                return result;
            }

            // Display an error message in the label
            errorLabel.Content = $"Invalid {prompt}. Try again.";
        }
    }
}
