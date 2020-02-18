using System.Threading.Tasks;
using FileSync.Shared.Models;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
    public class LoginViewModel : BaseMobileViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IConfigurationService _configurationService;
        private readonly INavigationService _navigationService;

        public LoginViewModel()
        {
            _loginService = DependencyService.Get<ILoginService>();
            _configurationService = DependencyService.Get<IConfigurationService>();
            _navigationService = DependencyService.Get<INavigationService>();
            LoginCommand = new Command(async () => await OnLogin());
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

        public Command LoginCommand { get; set; }

        private async Task OnLogin()
        {
            IsBusy = true;
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
                    //set error message
                }
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
