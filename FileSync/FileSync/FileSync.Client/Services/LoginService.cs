using System.Threading.Tasks;
using FileSync.Shared.Services;

namespace FileSync.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IRemoteLoginService _remoteLoginService;
        private readonly ILogger _logger;

        public LoginService(IConfigurationService configurationService, IRemoteLoginService remoteLoginService, ILogger logger)
        {
            _configurationService = configurationService;
            _remoteLoginService = remoteLoginService;
            _logger = logger;
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
                _logger.Log($"{nameof(LoginService)} - {e}");
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> Login(string mail, string password, string apiKey) => await _remoteLoginService.LoginAsync(mail, password, apiKey);

        public async Task Logout() => await _configurationService.ClearAsync();
    }
}
