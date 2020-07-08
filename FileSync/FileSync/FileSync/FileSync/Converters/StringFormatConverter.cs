using System;
using System.Globalization;
using Xamarin.Forms;

namespace FileSync.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter is string format)
            {
                var label = Resources.AppResources.ResourceManager.GetString(format);
                return string.Format(label, value);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
