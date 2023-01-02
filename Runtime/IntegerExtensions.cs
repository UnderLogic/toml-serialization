using System.Globalization;
using System.Text.RegularExpressions;

namespace UnderLogic.Serialization.Toml
{
    internal static class IntegerExtensions
    {
        private static readonly Regex HexNumberRegex =
            new(@"^0x([0-9a-f]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex OctNumberRegex =
            new(@"^0o([0-7]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex BinNumberRegex =
            new(@"^0b([0-1]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        public static bool TryParseHexNumber(string hexString, out long value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(hexString))
                return false;

            var hexMatch = HexNumberRegex.Match(hexString);
            if (!hexMatch.Success)
                return false;

            var hexValue = hexMatch.Groups[1].Value;
            return long.TryParse(hexValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value);
        }

        public static bool TryParseOctalNumber(string octalString, out long value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(octalString))
                return false;

            var octalMatch = OctNumberRegex.Match(octalString);
            if (!octalMatch.Success)
                return false;

            if (!long.TryParse(octalMatch.Groups[1].Value, out var octalValue))
                return false;
            
            var decValue = 0L;
            var baseValue = 1L;

            while (octalValue > 0)
            {
                var lastDigit = octalValue % 10;
                octalValue /= 10;
                
                decValue += lastDigit * baseValue;
                baseValue *= 8;
            }

            value = decValue;
            return true;
        }
        
        public static bool TryParseBinaryNumber(string binaryString, out long value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(binaryString))
                return false;

            var binaryMatch = BinNumberRegex.Match(binaryString);
            if (!binaryMatch.Success)
                return false;

            if (!long.TryParse(binaryMatch.Groups[1].Value, out var binaryValue))
                return false;
            
            var decValue = 0L;
            var baseValue = 1L;

            while (binaryValue > 0)
            {
                var lastDigit = binaryValue % 10;
                binaryValue /= 10;

                decValue += lastDigit * baseValue;
                baseValue <<= 1;
            }

            value = decValue;
            return true;
        }
    }
}
