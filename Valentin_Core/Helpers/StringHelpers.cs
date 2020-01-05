using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valentin_Core
{
    //TODO italic, bold, etc
    public static class StringHelpers
    {
        #region Public Static Methods

        /// <summary>
        /// Split strings by index. yields return the value 
        /// </summary>
        /// <param name="string">current string</param>
        /// <param name="indexes">markers for split</param>
        /// <returns>substrings</returns>
        public static IEnumerable<string> SplitByIndex(this string @string, params int[] indexes)
        {
            var previousIndex = 0;
            foreach (var index in indexes.OrderBy(i => i))
            {
                yield return @string.Substring(previousIndex, index - previousIndex);
                previousIndex = index;
            }

            yield return @string.Substring(previousIndex);

        }

        public static string CodeMessage(string received)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(received);
            sb.Insert(0, "`");
            sb.Insert(received.Length, "`");

            return sb.ToString();

        }
        #endregion
    }
}

