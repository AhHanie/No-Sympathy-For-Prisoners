using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_No_Sympathy_For_Prisoners
{
    public class Utils
    {
        /// <summary>
        /// Removes all lines starting from the first line that begins with the specified string.
        /// </summary>
        /// <param name="text">The input text</param>
        /// <param name="startString">The string to search for at the beginning of lines</param>
        /// <param name="caseSensitive">Whether the search should be case sensitive (default: true)</param>
        /// <returns>The text with lines removed starting from the matching line</returns>
        public static string RemoveLinesStartingFrom(string text, string startString, bool caseSensitive = true)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(startString))
                return text;

            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<string>();

            var comparison = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            foreach (var line in lines)
            {
                if (line.StartsWith(startString, comparison))
                {
                    // Found the target line - stop adding lines to result
                    break;
                }
                result.Add(line);
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}
