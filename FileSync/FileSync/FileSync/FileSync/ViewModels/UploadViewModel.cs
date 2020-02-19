using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FileSync.Services;
using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.ViewModels
{
	public class UploadViewModel : BaseMobileViewModel
    {
		public const string UploadFileMessageName = "UploadFiles";
		private readonly ISyncService _syncService;
		private readonly IShutDownService _shutDownService;
		private readonly INotificationService _notificationService;
		private readonly IFilesLocationService _filesLocationService;
		private readonly IToastNotificationService _toastNotificationService;

		public UploadViewModel()
		{
			_syncService = DependencyService.Get<ISyncService>();
			_shutDownService = DependencyService.Get<IShutDownService>();
			_notificationService = DependencyService.Get<INotificationService>();
			_filesLocationService = DependencyService.Get<IFilesLocationService>();
			_toastNotificationService = DependencyService.Get<IToastNotificationService>();


			MessagingCenter.Instance.Subscribe<object, object>(this, UploadFileMessageName, (context, intent) => 
			{
				Task.Run(async () => await UploadFilesAsync(context, intent));
			});
		}

		private double totalFiles;

		private double filesCount;

		public double FilesCount
		{
			get => filesCount;
			set => SetProperty(ref filesCount, value);
		}

		private double currentFileProgress;

		public double CurrentFileProgress
		{
			get => currentFileProgress;
			set => SetProperty(ref currentFileProgress, value);
		}

		private string progress;
				
		public string Progress
		{
			get => progress;
			set => SetProperty(ref progress, value);
		}

		private async Task UploadFilesAsync(object context, object intent)
		{
			try
			{
				var filesToSend =  _filesLocationService.GetActualPathForFiles(intent, context);
				totalFiles = filesToSend.Count();
				UpdateProgress(default, default);

				await Task.Delay(400); //simple workaround for locked files in android filesystem
				var result = await _syncService.UploadFilesAsync(filesToSend, UpdateProgress);
				_notificationService.Notify("Files Upload", result ? "Files uploaded successfully" : "Cannot upload files");
				_toastNotificationService.ShowToast(result ? "Files uploaded successfully" : "Cannot upload files");
			}
			catch (System.Exception e)
			{
				_notificationService.Notify("Files Upload", "Failed to upload files");
				Debug.WriteLine(e.ToString());
			}
			finally
			{
				await Task.Delay(300); //simple workaround to notify user before quit
//#if !DEBUG
				_shutDownService.CloseApp();
//#endif
			}
		}

		private void UpdateProgress(int counter, int progress)
		{
			Debug.WriteLine($"File upload progress. Current file: {counter}. Percent: {progress} %");
			FilesCount = (double)counter / totalFiles;
			Progress = $"{counter}/{totalFiles}";
			CurrentFileProgress = progress;
		}
	}
}
