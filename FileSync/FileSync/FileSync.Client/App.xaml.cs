using System;
using System.Linq;
using System.Windows;
using FileSync.Client.Helpers;
using FileSync.Client.Services;

namespace FileSync.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                
                if (e.Args.Length > 0 && System.IO.File.Exists(e.Args[0]))
                {
                    StartupUri = new Uri("UploadWindow.xaml", UriKind.RelativeOrAbsolute);
                    return;
                }

                ShellExtension.Process(e.Args.Any() ? e.Args[0] : string.Empty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                new Logger().Log($"Startup error: {ex}");
                Current.Shutdown();
            }
        }
    }
}
