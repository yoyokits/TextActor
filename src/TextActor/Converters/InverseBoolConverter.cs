namespace TextActor.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the <see cref="InverseBoolConverter" />.
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        #region Properties

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static InverseBoolConverter Instance { get; } = new InverseBoolConverter();

        #endregion Properties

        #region Methods

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        #endregion Methods
    }
}