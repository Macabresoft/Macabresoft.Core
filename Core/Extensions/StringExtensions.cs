namespace Macabresoft.Core {

    using System;
    using System.Linq;

    /// <summary>
    /// Extensions for <see cref="string" />.
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Determines whether the text contains the provided value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="value">The value.</param>
        /// <param name="stringComparisonType">Type of the string comparison.</param>
        /// <returns><c>true</c> if the text contains the specified value; otherwise, <c>false</c>.</returns>
        public static bool Contains(this string text, string value, StringComparison stringComparisonType) {
            return text.Contains(value, stringComparisonType);
        }

        /// <summary>
        /// Converts a string to a file safe string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The file safe string.</returns>
        public static string ToSafeString(this string value) {
            return value != null ? new string(value.Where(char.IsLetterOrDigit).ToArray()) : string.Empty;
        }
    }
}