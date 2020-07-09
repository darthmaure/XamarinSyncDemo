using Autofac;
using FileSync.Client.Services;
using FileSync.Shared.Services;

namespace FileSync.Client.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Logger>().As<ILogger>();
            builder.RegisterType<LoginService>().As<ILoginService>();
            builder.RegisterType<RemoteLoginService>().As<IRemoteLoginService>();
            builder.RegisterType<ConfigurationService>().As<IConfigurationService>();
            builder.RegisterType<SyncService>().As<ISyncService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<DownloadService>().As<IDownloadService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<ToastNotificationManager>().As<INotificationManager>();
            builder.RegisterType<ToastNotificationService>().As<IToastNotificationService>();

            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<ItemsViewModel>().SingleInstance();
            builder.RegisterType<ShellViewModel>().SingleInstance();
            builder.RegisterType<UploadViewModel>().SingleInstance();

            Container = builder.Build();
        }

        public static IContainer Container { get; set; }

        public ShellViewModel Shell => Container.Resolve<ShellViewModel>();

        public UploadViewModel Upload => Container.Resolve<UploadViewModel>();
    }
}
