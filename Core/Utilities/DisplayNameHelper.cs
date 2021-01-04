namespace Macabresoft.Core {
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///     Helper class that uses the <see cref="DisplayAttribute" /> to get display names.
    /// </summary>
    public static class DisplayNameHelper {
        /// <summary>
        ///     Gets the display name for an <see cref="Enum" /> value.
        /// </summary>
        /// <param name="value">The <see cref="Enum" /> value.</param>
        /// <returns>The display name or the name of the enum if no display name is specified.</returns>
        public static string GetEnumDisplayName(this Enum value) {
            var result = string.Empty;

            if (value.GetType().GetField(value.ToString()) is FieldInfo fieldInfo) {
                var attribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().FirstOrDefault();
                ;
                result = attribute != null ? attribute.Name : value.ToString();
            }

            return result;
        }

        /// <summary>
        ///     Gets the display name for the property of a <see cref="Type" />.
        /// </summary>
        /// <param name="owningType">The owning type.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The display name.</returns>
        public static string GetPropertyDisplayName(this Type owningType, string propertyName) {
            var result = string.Empty;
            if (owningType != null && !string.IsNullOrEmpty(propertyName)) {
                if (owningType.GetProperty(propertyName) is PropertyInfo propertyInfo &&
                    propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().FirstOrDefault() is DisplayAttribute attribute) {
                    result = attribute.Name;
                }
                else {
                    result = propertyName;
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the display name for a <see cref="Type" />.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The display name or the name of the type if no display name is specified.</returns>
        public static string GetTypeDisplayName(this Type type) {
            var result = string.Empty;

            if (type != null) {
                var attribute = type.GetCustomAttributes(typeof(DisplayAttribute), false).OfType<DisplayAttribute>().FirstOrDefault();
                result = attribute != null ? attribute.Name : type.Name;
            }

            return result;
        }
    }
}