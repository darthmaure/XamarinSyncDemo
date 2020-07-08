using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Client.Helpers;
using FileSync.Shared.Services;
using FileSync.Shared.ViewModels;

namespace FileSync.Client.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
		private readonly ILoginService _loginService;
		private readonly INavigationService _navigationService;

		public ShellViewModel(ILoginService loginService, INavigationService navigationService)
		{
			_loginService = loginService;
			_navigationService = navigationService;

			LoadedCommand = new RelayCommand(async p => await OnLoaded(), p => true);
		}

		private BaseClientViewModel current;

		public BaseClientViewModel Current
		{
			get => current;
			set => SetProperty(ref current, value);
		}

		private bool isLoggedIn;

		public bool IsLoggedIn
		{
			get => isLoggedIn;
			set => SetProperty(ref isLoggedIn, value);
		}

		public ICommand LoadedCommand { get; }
		
		private async Task OnLoaded() 
		{
			var isLoggedIn = await _loginService.IsLoggedIn();
			if (isLoggedIn)
			{
				await _navigationService.NavigateHomeAsync();
			}
			else 
			{
				await _navigationService.NavigateLoginAsync();
			}
		}
	}
}
