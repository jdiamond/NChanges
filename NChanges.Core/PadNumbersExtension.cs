using System.Text.RegularExpressions;

namespace NChanges.Core
{
    public static class PadNumbersExtension
    {
        private static readonly Regex NumbersRegex = new Regex(@"\d+");

        public static string PadNumbers(this string value)
        {
            return NumbersRegex.Replace(value, m => m.Value.PadLeft(10, '0'));
        }
    }
}