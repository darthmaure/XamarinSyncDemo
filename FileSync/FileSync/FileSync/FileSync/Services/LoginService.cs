using System.Threading.Tasks;
using FileSync.Shared.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FileSync.Services
{
    public class LoginService : ILoginService
    {
        private const string _loginFlagKey = "isUserLoggedIn";
        private const string _loggedFlagValue = "1";
        private readonly IConfigurationService _configurationService;
        private readonly IRemoteLoginService _remoteLoginService;

        public LoginService()
        {
            _configurationService = DependencyService.Get<IConfigurationService>();
            _remoteLoginService = DependencyService.Get<IRemoteLoginService>();
        }


        public async Task<bool> IsLoggedIn()
        {
            var isUserLoggedIn = await SecureStorage.GetAsync(_loginFlagKey);
            return isUserLoggedIn == _loggedFlagValue;
        }

        public async Task<bool> Login(string mail, string password, string apiKey)
        {
            var loginResult = await _remoteLoginService.LoginAsync(mail, password, apiKey);

            if (loginResult)
            {
                await SecureStorage.SetAsync(_loginFlagKey, _loggedFlagValue);
            }
            return loginResult; 
        }

        public async Task Logout()
        {
            await _configurationService.ClearAsync();
            SecureStorage.Remove(_loginFlagKey);
        }
    }
}