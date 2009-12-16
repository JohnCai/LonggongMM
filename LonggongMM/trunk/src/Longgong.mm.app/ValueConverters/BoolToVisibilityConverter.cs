using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Longgong.mm.app
{
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public class BoolToVisibilityConverter: IValueConverter
    {
        #region Implementation of IValueConverter
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return Binding.DoNothing;
            }

            bool input = false;
            bool.TryParse(value.ToString(), out input);

            bool invertActive = false;
            bool.TryParse(parameter.ToString(), out invertActive);

            if (input)
            {
                return invertActive ? Visibility.Visible : Visibility.Collapsed;
            }
            return invertActive ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}