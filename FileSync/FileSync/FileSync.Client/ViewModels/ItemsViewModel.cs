using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FileSync.Client.Helpers;
using FileSync.Client.Services;
using FileSync.Shared.Models;
using FileSync.Shared.Services;

namespace FileSync.Client.ViewModels
{
    public class ItemsViewModel : BaseClientViewModel
	{
		private readonly ISyncService _syncService;
		private readonly IDownloadService _downloadService;
		private readonly IDialogService _dialogService;
		private readonly ILoginService _loginService;
		private readonly INavigationService _navigationService;
		private readonly IConfigurationService _configurationService;


		public ItemsViewModel(
			ISyncService syncService, 
			IDownloadService downloadService, 
			IDialogService dialogService, 
			ILoginService loginService, 
			INavigationService navigationService, 
			IConfigurationService configurationService)
		{
			_syncService = syncService;
			_downloadService = downloadService;
			_dialogService = dialogService;
			_loginService = loginService;
			_navigationService = navigationService;
			_configurationService = configurationService;

			RefreshCommand = new RelayCommand(async p => await OnNavigated(), p => !IsBusy);
			DeleteCommand = new RelayCommand(async p => await OnDeleteItem(), p => !IsBusy && SelectedItem != null);
			DownloadCommand = new RelayCommand(async p => await OnDownloadItem(), p => !IsBusy && SelectedItem != null);
			LogoutCommand = new RelayCommand(async p => await OnLogout(), p => !IsBusy);
			UploadCommand = new RelayCommand(async p => await OnUpload(p), p => !IsBusy);
		}

		private ObservableCollection<SyncItem> items;

		public ObservableCollection<SyncItem> Items
		{
			get => items;
			set => SetProperty(ref items, value);
		}

		private SyncItem selectedItem;

		public SyncItem SelectedItem
		{
			get => selectedItem;
			set => SetProperty(ref selectedItem, value);
		}

		private long totalSize;

		public long TotalSize
		{
			get => totalSize;
			set => SetProperty(ref totalSize, value);
		}

		public ICommand RefreshCommand { get; }
		public ICommand DownloadCommand { get; }
		public ICommand DeleteCommand { get; }
		public ICommand LogoutCommand { get; }
		public ICommand UploadCommand { get; }

		public override async Task OnNavigated()
		{
			IsBusy = true;
			Items = null;
			TotalSize = default;
			Message = null;
			try
			{
				var items = await _syncService.GetFilesAsync();

				if (items?.Count() > 0)
				{
					Items = new ObservableCollection<SyncItem>(items.OrderByDescending(d => d.CreateDate));
					TotalSize = Items.Sum(d => d.Length);
				}
				else
				{
					Message = "No items.";
				}
			}
			catch (System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
				Message = "Failed to show items";
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async Task OnDeleteItem() 
		{
			if (SelectedItem == null) return;
			Message = null;

			if (!_dialogService.ShowConfirmationDialog("Delete file", $"Are you sure you want to delete file {SelectedItem.Name}?")) return;
			IsBusy = true;
			try
			{
				var result = await _syncService.DeleteFileAsync(SelectedItem);
				Message = result ? $"File deleted {SelectedItem.Name}" : "Cannot delete file";

				await OnNavigated();
			}
			catch (System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
				Message = "Cannot delete file";
			}
			finally
			{
				IsBusy = false;
			}
		}
		private async Task OnDownloadItem() 
		{
			if (SelectedItem == null) return;
			Message = null;

			var destinationFile = _dialogService.SaveFile(SelectedItem.Name);
			if (string.IsNullOrEmpty(destinationFile)) return;

			IsBusy = true;
			try
			{
				var url = await _syncService.GetDownloadFileUrlAsync(SelectedItem.Name);
				await _downloadService.DownloadFile(url, destinationFile, true);
			}
			catch (System.Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
				Message = "Cannot download file";
			}
			finally
			{
				IsBusy = false;
			}
		}
		private async Task OnLogout()
		{
			await _loginService.Logout();
			await _configurationService.ClearAsync();
			await _navigationService.NavigateLoginAsync();
		}
		private async Task OnUpload(object parameter)
		{
			if (parameter != null && parameter is IEnumerable<string> files)
			{
				IsBusy = true;
				try
				{
					await _syncService.UploadFilesAsync(files, (x, y) => 
					{
						var filename = System.IO.Path.GetFileName(files.ElementAt(x));
						UploadingMessage = $"Uploading file {filename}";
					});
				}
				catch (System.Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e);
				}
				finally
				{
					UploadingMessage = null;
					IsBusy = false;
				}
				await OnNavigated(); //refresh
			}
		}
	}
}
