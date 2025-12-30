using CourtInvitor.Models;
using System.Globalization;
namespace CourtInvitor.Converters
{
    /// <summary>
    /// Converts a boolean value to a visibility icon.
    /// </summary>
    internal class BoolToIconConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean to a visibility icon string.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">Optional parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>The visibility icon string.</returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string icon = Icons.Visibility_off;
            if (value != null)
                icon = (bool)value ? Icons.Visibility_off : Icons.Visibility_on;
            return icon;
        }
        /// <summary>
        /// Converts back from icon to boolean. Not implemented.
        /// </summary>
        /// <param name="value">The icon value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">Optional parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Null as this conversion is not supported.</returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
