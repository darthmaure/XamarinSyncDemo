using System.Threading.Tasks;
using FileSync.Client.Services;
using FileSync.Shared.Services;

namespace FileSync.Client.ViewModels
{
    public class ShellViewModel : BaseMainViewModel
	{
		private readonly INavigationService _navigationService;

		public ShellViewModel(
			ILoginService loginService, 
			INotificationManager notificationManager, 
			INavigationService navigationService) : base(loginService, notificationManager)
		{
			_navigationService = navigationService;
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

		protected override async Task OnLoaded()
		{
			_notificationManager.CreateNotificationArea();

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
