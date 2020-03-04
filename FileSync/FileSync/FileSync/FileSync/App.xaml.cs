using FileSync.Services;
using FileSync.Shared.Services;
using FileSync.ViewModels;
using FileSync.Views;
using Xamarin.Forms;

namespace FileSync
{
    public partial class App : Application
    {

        public App(bool isSendingFiles)
        {
            InitializeComponent();

            Shared.Services.SimpleServiceLocator.Default = new Services.SimpleServiceLocator();

            DependencyService.Register<IConfigurationService, ConfigurationService>();
            DependencyService.Register<IRemoteLoginService, RemoteLoginService>();
            DependencyService.Register<ILoginService, LoginService>();
            DependencyService.Register<INavigationService, NavigationService>();
            DependencyService.Register<ISyncService, SyncService>();
            DependencyService.Register<IDeleteItemService, DeleteItemService>();
            DependencyService.Register<IDownloadItemService, DownloadItemService>();
            DependencyService.Register<IDateToHeaderFormatService, DateToHeaderFormatService>();
            DependencyService.Register<ItemsViewModel>();
            DependencyService.Register<LoginViewModel>();
            DependencyService.Register<AboutViewModel>();
            DependencyService.Register<UploadViewModel>();

            if (isSendingFiles)
            {
                MainPage = new UploadView { BindingContext = DependencyService.Get<UploadViewModel>() };
            }
            else
            {
                var navigationService = DependencyService.Get<INavigationService>();
                var loginService = DependencyService.Resolve<ILoginService>();
                var isUserLoggedIn = loginService.IsLoggedIn().Result;
                if (isUserLoggedIn)
                {
                    navigationService.NavigateHomeAsync();
                }
                else
                {
                    MainPage = new LoginPage { BindingContext = DependencyService.Get<LoginViewModel>() };
                }

            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
