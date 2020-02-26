using System.ComponentModel;
using System.Threading.Tasks;
using FileSync.Shared.ViewModels;

namespace FileSync.ViewModels
{
    public class BaseMobileViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public virtual Task OnAppearing() => Task.CompletedTask;
    }
}
