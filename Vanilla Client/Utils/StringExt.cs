using System.Text.RegularExpressions;

namespace Vanilla.Utils
{
    public static class StringExt
    {
        public static string StripHtml(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Use regex to remove HTML tags
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        public static string StripHtml(this object input)
        {
            if (string.IsNullOrEmpty(input.ToString()))
            {
                return input.ToString();
            }

            // Use regex to remove HTML tags
            return Regex.Replace(input.ToString(), "<.*?>", string.Empty);
        }
    }
}