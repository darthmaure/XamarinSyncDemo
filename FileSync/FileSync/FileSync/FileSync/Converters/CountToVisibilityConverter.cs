using System;
using System.Globalization;
using Xamarin.Forms;

namespace FileSync.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return value != null && value is long l && l > 0;
            else //inverted
                return value != null && value is long l && l == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
