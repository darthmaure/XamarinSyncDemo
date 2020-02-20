using System.Threading.Tasks;
using Autofac;
using FileSync.Client.ViewModels;
using FileSync.Shared.Services;

namespace FileSync.Client.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task NavigateHomeAsync() => await NavigateCore<ItemsViewModel>();

        public async Task NavigateLoginAsync() => await NavigateCore<LoginViewModel>();

        private async Task NavigateCore<T>() where T : BaseClientViewModel
        {
            var shell = ViewModelLocator.Container.Resolve<ShellViewModel>();
            var current = ViewModelLocator.Container.Resolve<T>();
            shell.Current = current;
            await current.OnNavigated();
        }
    }
}
