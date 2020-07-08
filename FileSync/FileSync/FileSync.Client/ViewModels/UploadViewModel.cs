using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FileSync.Client.Helpers;
using FileSync.Shared.Services;
using FileSync.Shared.ViewModels;
using Resource = FileSync.Client.Resources.AppResources;


namespace FileSync.Client.ViewModels
{
    public class UploadViewModel : BaseViewModel
    {
        private readonly ISyncService _syncService;
        private readonly ILoginService _loginService;
        private readonly ILogger _logger;

        public UploadViewModel(
            ISyncService syncService, 
            ILoginService loginService,
            ILogger logger)
        {
            _syncService = syncService;
            _loginService = loginService;
            _logger = logger;

            LoadedCommand = new RelayCommand(async p => await OnLoaded(), p => true);
        }

        public ICommand LoadedCommand { get; }

        private string uploadingMessage;

        public string UploadingMessage
        {
            get => uploadingMessage;
            set => SetProperty(ref uploadingMessage, value);
        }

        private async Task OnLoaded()
        {
            try
            {
                var isLoggedIn = await _loginService.IsLoggedIn();

                if (!isLoggedIn)
                {
                    //todo: show toast
                }
                else
                {
                    var files = Environment.GetCommandLineArgs().Skip(1);

                    await _syncService.UploadFilesAsync(files, (x, y) =>
                    {
                        var filename = System.IO.Path.GetFileName(files.ElementAt(x - 1));
                        UploadingMessage = string.Format(Resource.UploadFilesMessage, filename);
                    });
                }
            }
            catch (Exception e)
            {
                _logger.Log($"{e}");
            }
            finally
            {
                await Task.Delay(1000);
                UploadingMessage = null;
                Application.Current.Shutdown();
                _logger.Log($"{GetType().Name} - Close");
            }
        }

    }
}