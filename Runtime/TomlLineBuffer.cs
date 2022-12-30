using System.Collections.Generic;
using System.Text;

namespace UnderLogic.Serialization.Toml
{
    internal class TomlLineBuffer
    {
        private readonly StringBuilder _lineBuffer = new(1024);
        private readonly StringBuilder _tempBuffer = new(1024);
        
        public void AppendLine(string value) => _lineBuffer.AppendLine(value);

        public IEnumerable<string> GetTomlLines()
        {
            var line = DequeueNextLine();

            while (line != null)
            {
                var tomlLine = StripComment(line);
                if (!string.IsNullOrWhiteSpace(tomlLine))
                    yield return tomlLine;
                
                line = DequeueNextLine();
            }
        }

        private string DequeueNextLine()
        {
            _tempBuffer.Clear();

            var singleQuoteCount = 0;
            var doubleQuoteCount = 0;
            var inMultiLineString = false;
            var inBasicString = false;
            var inLiteralString = false;
            var ignoreWhitespace = false;
            
            for (var charIndex = 0; charIndex < _lineBuffer.Length; charIndex++)
            {
                var currentChar = _lineBuffer[charIndex];
                var nextChar = charIndex + 1 < _lineBuffer.Length ? _lineBuffer[charIndex + 1] : '\0';

                var isBackslash = currentChar == '\\';
                var isSingleQuote = currentChar == '\'';
                var isDoubleQuote = currentChar == '"';
                var isNewLine = currentChar == '\r' || currentChar == '\n';

                // Reset the ignore whitespace flag if we encounter a non-whitespace character
                if (!char.IsWhiteSpace(currentChar))
                    ignoreWhitespace = false;

                var suppressThisChar = ignoreWhitespace && char.IsWhiteSpace(currentChar);
                
                if (isSingleQuote)
                    singleQuoteCount++;
                else
                    singleQuoteCount = 0;
                
                if (isDoubleQuote)
                    doubleQuoteCount++;
                else
                    doubleQuoteCount = 0;

                if (singleQuoteCount == 3 && !inBasicString)
                {
                    inMultiLineString = !inMultiLineString;
                    inLiteralString = !inLiteralString;
                    
                    // Skip the next newline at the start of a multiline string
                    if (inMultiLineString && (nextChar == '\r' || nextChar == '\n'))
                        charIndex++;
                }

                if (doubleQuoteCount == 3 && !inLiteralString)
                {
                    inMultiLineString = !inMultiLineString;
                    inBasicString = !inBasicString;

                    // Skip the next newline at the start of a multiline string
                    if (inMultiLineString && (nextChar == '\r' || nextChar == '\n'))
                        charIndex++;
                }
                
                // Unescaped backslash in a multiline string will ignore all whitespace until the next non-whitespace character
                // This does not apply to literal strings
                if (!inLiteralString && isBackslash && inMultiLineString)
                {
                    if (char.IsWhiteSpace(nextChar) || nextChar == '\0')
                    {
                        // Suppress the backslash from being added to the buffer
                        suppressThisChar = true;
                        ignoreWhitespace = true;
                    }
                }

                if (isNewLine && !inMultiLineString)
                {
                    var line = _tempBuffer.ToString();
                    _lineBuffer.Remove(0, charIndex + 1);
                    return line;
                }

                if (!suppressThisChar)
                    _tempBuffer.Append(currentChar);
            }
            
            return null;
        }

        private static string StripComment(string line)
        {
            if (line.StartsWith("#"))
                return string.Empty;
            
            var inDoubleQuotes = false;
            var inSingleQuotes = false;

            for (var charIndex = 0; charIndex < line.Length; charIndex++)
            {
                var eachChar = line[charIndex];

                if (eachChar == '"')
                    inDoubleQuotes = !inDoubleQuotes;

                if (eachChar == '\'')
                    inSingleQuotes = !inSingleQuotes;

                if (!inDoubleQuotes && !inSingleQuotes && eachChar == '#')
                    return line.Substring(0, charIndex);
            }

            return line;
        }
    }
}
