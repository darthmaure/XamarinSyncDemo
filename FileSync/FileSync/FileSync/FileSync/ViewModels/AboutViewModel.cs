using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Shared.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class AboutViewModel : BaseMobileViewModel
    {
        private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;

        public AboutViewModel()
        {
            _loginService = DependencyService.Get<ILoginService>();
            _navigationService = DependencyService.Get<INavigationService>();

            LogoutCommand = new Command(async () => await Logout());
        }

        public ICommand LogoutCommand { get; }

        private async Task Logout()
        {
            IsBusy = true;
            try
            {
                await _loginService.Logout();
                await _navigationService.NavigateLoginAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            IsBusy = false;
        }
    }
}