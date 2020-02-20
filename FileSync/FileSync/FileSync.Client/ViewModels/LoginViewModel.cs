using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Client.Helpers;
using FileSync.Shared.Models;
using FileSync.Shared.Services;

namespace FileSync.Client.ViewModels
{
    public class LoginViewModel : BaseClientViewModel
	{
        private readonly ILoginService _loginService;
        private readonly IConfigurationService _configurationService;
        private readonly INavigationService _navigationService;

        public LoginViewModel(
            ILoginService loginService, 
            IConfigurationService configurationService, 
            INavigationService navigationService)
        {
            _loginService = loginService;
            _configurationService = configurationService;
            _navigationService = navigationService;

            LoginCommand = new RelayCommand(async (p) => await OnLogin(), p => CanLogin());
        }

        private string email;

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string apiKey;

        public string ApiKey
        {
            get => apiKey;
            set => SetProperty(ref apiKey, value);
        }

        private string database;

        public string Database
        {
            get => database;
            set => SetProperty(ref database, value);
        }

        private string bucket;

        public string Bucket
        {
            get => bucket;
            set => SetProperty(ref bucket, value);
        }

        public ICommand LoginCommand { get; set; }

        public override async Task OnNavigated() => await Task.CompletedTask;
        private async Task OnLogin() 
        {
            IsBusy = true;
            Message = null;
            try
            {
                var loginResult = await _loginService.Login(Email, Password, ApiKey);
                if (loginResult)
                {
                    await _configurationService.SaveAsync(new Configuration
                    {
                        ApiKey = ApiKey,
                        Bucket = Bucket,
                        DatabaseUrl = Database,
                        Email = Email,
                        Password = Password
                    });

                    //clear inputs
                    ApiKey = null;
                    Bucket = null;
                    Database = null;
                    Email = null;
                    Password = null;

                    await _navigationService.NavigateHomeAsync();
                }
                else
                {
                    Message = "Login failed.";
                }
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
        private bool CanLogin()
        {
            return true;
        }
    }
}
