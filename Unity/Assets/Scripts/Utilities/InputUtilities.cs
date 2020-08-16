using System.Linq;

namespace CalorieCounter.Utilities
{
    public static class InputUtilities
    {
        private static readonly char[] ValidSpecialChars = new char[]
        {
                        '-', '\'', '&', '.', ' ', '/', '%',
        };

        // Unused parameters are necessary for TMP_InputField.OnValidateInput()
        public static char ValidateNonDecimalInput(string text, int charIndex, char addedChar)
        {
            return char.IsLetterOrDigit(addedChar) || ValidSpecialChars.Contains(addedChar) ? addedChar : '\0';
        }
    }
}
