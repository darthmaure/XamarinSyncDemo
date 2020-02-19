using System;
using System.Globalization;
using Xamarin.Forms;

namespace FileSync.Converters
{
    public class FileNameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var file = value?.ToString();
            if (string.IsNullOrEmpty(file)) return string.Empty;

            var extension = System.IO.Path.GetExtension(file);
            switch (extension)
            {
                case ".zip":
                case ".rar":
                case ".7z":
                    return "outline_archive_white_36dp.png";
                case ".png":
                case ".jpg":
                case ".bmp":
                case ".jpeg":
                case ".gif":
                case ".tiff":
                    return "outline_picture_as_pdf_white_36dp.png";
                case ".avi":
                case ".flv":
                case ".wmv":
                case ".mov":
                case ".mp4":
                case ".mkv":
                case ".3gp":
                    return "outline_video_library_white_36dp.png";
                case ".mobi":
                case ".epub":
                    return "outline_menu_book_white_36dp.png";
                case ".mp3":
                case ".aac":
                case ".flac":
                case ".raw":
                case ".wav":
                case ".wma":
                    return "outline_library_music_white_36dp.png";
                case ".pdf":
                    return "outline_picture_as_pdf_white_36dp.pdf";
                case ".txt":
                case ".doc":
                case ".docx":
                case ".rtf":
                case ".odt":
                    return "outline_insert_drive_file_white_36dp.png";
                default:
                    return "outline_cloud_upload_black_18dp.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
