using System;
using System.Reflection;
using FileSync.Client.Services;
using Microsoft.Win32;
using Resource = FileSync.Client.Resources.AppResources;

namespace FileSync.Client.Helpers
{
    public static class ShellExtension
    {
        public static void Process(string arg)
        {
            var logger = new Logger();
            try
            {
                var shellKeyName = Resource.ShellKeyName;
                var menuText = Resource.ShellMenuText;
                var menuCommand = string.Format("\"{0}\" \"%1\"", GetExecutableFullName());
                var regPath = string.Format(@"*\shell\{0}", shellKeyName);

                if (string.IsNullOrEmpty(arg))
                {
                    logger.Log("registering");

                    using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(regPath))
                    {
                        key.SetValue(null, menuText);
                    }

                    using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(string.Format(@"{0}\command", regPath)))
                    {
                        key.SetValue(null, menuCommand);
                    }
                }
                else if (arg.Equals("u"))
                {
                    logger.Log("unregistering");
                    Registry.ClassesRoot.DeleteSubKeyTree(regPath);
                }
            }
            catch (Exception e)
            {
                logger.Log($"{nameof(ShellExtension)} error: {e}");
            }
        }

        private static string GetExecutableFullName() => Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");
    }
}
