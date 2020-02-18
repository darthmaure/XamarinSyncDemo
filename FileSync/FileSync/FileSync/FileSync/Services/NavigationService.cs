using System.Threading.Tasks;
using FileSync.Shared.Services;
using FileSync.Views;
using Xamarin.Forms;

namespace FileSync.Services
{
    public class NavigationService : INavigationService
    {
        public const string HomeNavigationPath = "main";

        public Task NavigateAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task NavigateHomeAsync()
        {
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync($"//{HomeNavigationPath}");
        }

        public async Task NavigateLoginAsync()
        {
            if (Shell.Current != null)
                Shell.Current.FlyoutIsPresented = false;
            App.Current.MainPage = new LoginPage { BindingContext = DependencyService.Get<ViewModels.LoginViewModel>() };
            await Task.CompletedTask;
        }
    }
}