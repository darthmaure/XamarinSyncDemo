using System;
using System.Globalization;
using FileSync.Shared.Converters;
using Xamarin.Forms;

namespace FileSync.Converters
{
    public class FileSizeFormatConverter : IValueConverter
    {
        private readonly FileSizeConverter _fileSizeConverter = new FileSizeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is long d) return _fileSizeConverter.ToFileSize(d);
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
