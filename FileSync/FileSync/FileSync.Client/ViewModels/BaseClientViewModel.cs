using System.Threading.Tasks;
using FileSync.Shared.ViewModels;

namespace FileSync.Client.ViewModels
{
    public abstract class BaseClientViewModel : BaseViewModel
	{
		public abstract Task OnNavigated();

        private string message;

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private string uploadingMessage;

        public string UploadingMessage
        {
            get => uploadingMessage;
            set => SetProperty(ref uploadingMessage, value);
        }
    }
}
