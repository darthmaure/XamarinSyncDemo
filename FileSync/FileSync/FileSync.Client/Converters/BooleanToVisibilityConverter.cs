using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FileSync.Client.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (value != null && value is bool b && b) ? Visibility.Visible : Visibility.Collapsed;
            else //inverted
                return (value != null && value is bool b && !b) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
