using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KendirStudios.CustomPackages.Utilities.Extensions
{
    public static class StringExtensions
    {
        private readonly static List<string> blacklist = new()
        { "fuck", "shit", "bitch", "damn", "negro", "nigga", "nigger" };

        /// <summary>
        /// Takes a string, checks if it contains any of the blacklisted words, and if so, replaces that substring by the respective amount of *, then returns it
        /// </summary>
        public static string Censor(this string parent)
        {
            blacklist.ForEach(cussWord =>
            {
                if (!parent.Contains(cussWord))
                {
                    return;
                }

                string censorWord = cussWord[0].ToString();
                for (int i = 1; i < cussWord.Length; i++)
                {
                    censorWord += '*';
                }

                parent = parent.Replace(cussWord, censorWord);
            });

            return parent;
        }

        /// <summary>
        /// Remove all whitespace (spaces, tabs, newlines, etc.).
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static string RemoveWhitespace(this string parent)
        {
            return Regex.Replace(parent, @"\s+", "");
        }

        /// <summary>
        /// Verifies if string is a valid email
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string parent)
        {
            if (string.IsNullOrWhiteSpace(parent))
            {
                return false;
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(parent, pattern, RegexOptions.IgnoreCase);
        }
    }
}