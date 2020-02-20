using System.Threading.Tasks;
using FileSync.Shared.Services;

namespace FileSync.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IRemoteLoginService _remoteLoginService;

        public LoginService(IConfigurationService configurationService, IRemoteLoginService remoteLoginService)
        {
            _configurationService = configurationService;
            _remoteLoginService = remoteLoginService;
        }

        public async Task<bool> IsLoggedIn()
        {
            try
            {
                var configration = await _configurationService.LoadAsync();
                return await _remoteLoginService.LoginAsync(configration.Email, configration.Password, configration.ApiKey);
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> Login(string mail, string password, string apiKey) => await _remoteLoginService.LoginAsync(mail, password, apiKey);

        public async Task Logout() => await _configurationService.ClearAsync();
    }
}
