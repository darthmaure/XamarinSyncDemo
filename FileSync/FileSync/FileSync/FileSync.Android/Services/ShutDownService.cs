using System;
using Android.OS;
using FileSync.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSync.Droid.Services.ShutDownService))]
namespace FileSync.Droid.Services
{
    public class ShutDownService : IShutDownService
    {
        public void CloseApp()
        {
            try
            {
                Process.KillProcess(Process.MyPid());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"[CLOSING APP]{e}");
            }
        }
    }
}