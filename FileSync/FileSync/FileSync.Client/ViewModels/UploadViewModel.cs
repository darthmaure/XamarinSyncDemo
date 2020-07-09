using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FileSync.Client.Services;
using FileSync.Shared.Services;
using Resource = FileSync.Client.Resources.AppResources;


namespace FileSync.Client.ViewModels
{
    public class UploadViewModel : BaseMainViewModel
    {
        private readonly ISyncService _syncService;
        private readonly IToastNotificationService _toastNotificationService;
        private readonly ILogger _logger;

        public UploadViewModel(
            ILoginService loginService,
            INotificationManager notificationManager,
            ISyncService syncService, 
            IToastNotificationService toastNotificationService,
            ILogger logger) : base(loginService, notificationManager)
        {
            _syncService = syncService;
            _toastNotificationService = toastNotificationService;
            _logger = logger;
        }


        private string uploadingMessage;

        public string UploadingMessage
        {
            get => uploadingMessage;
            set => SetProperty(ref uploadingMessage, value);
        }

        protected override async Task OnLoaded()
        {
            try
            {
                _notificationManager.CreateNotificationArea();

                var isLoggedIn = await _loginService.IsLoggedIn();
                if (!isLoggedIn)
                {
                    _toastNotificationService.ShowToast(Resource.UserNotLoggedInMessage, true);
                }
                else
                {
                    var files = Environment.GetCommandLineArgs().Skip(1);

                    await _syncService.UploadFilesAsync(files, (x, y) =>
                    {
                        var filename = System.IO.Path.GetFileName(files.ElementAt(x - 1));
                        UploadingMessage = string.Format(Resource.UploadFilesMessage, filename);
                    });
                    _toastNotificationService.ShowToast(Resource.FilesUploadedMessage);
                }
            }
            catch (Exception e)
            {
                _logger.Log($"{e}");
                _toastNotificationService.ShowToast(Resource.UploadFailedMessage, true);
            }
            finally
            {
                await Task.Delay(2000);
                UploadingMessage = null;
                Application.Current.Shutdown();
                _logger.Log($"{GetType().Name} - Close");
            }
        }

    }
}