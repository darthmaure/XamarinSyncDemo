using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Client.Helpers;
using FileSync.Client.Services;
using FileSync.Shared.Services;
using FileSync.Shared.ViewModels;


namespace FileSync.Client.ViewModels
{
    public abstract class BaseMainViewModel : BaseViewModel
    {
        protected readonly ILoginService _loginService;
        protected readonly INotificationManager _notificationManager;

        public BaseMainViewModel(ILoginService loginService, INotificationManager notificationManager)
        {
            _loginService = loginService;
            _notificationManager = notificationManager;

            LoadedCommand = new RelayCommand(async p => await OnLoaded(), p => true);
        }

        public ICommand LoadedCommand { get; }

        protected abstract Task OnLoaded();
    }
}