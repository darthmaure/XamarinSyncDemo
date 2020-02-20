using System.Windows;
using Microsoft.Win32;

namespace FileSync.Client.Services
{
    public class DialogService : IDialogService
    {
        public string OpenFile()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                Title = "Select file to upload",
                ValidateNames = true
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string SaveFile(string filename)
        {
            var dialog = new SaveFileDialog
            {
                CheckPathExists = true,
                Title = "Download file destination",
                FileName = filename,
                ValidateNames = true
            };
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public bool ShowConfirmationDialog(string title, string message)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }
}
