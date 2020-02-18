using FileSync.Shared.Services;
using Xamarin.Forms;

namespace FileSync.Services
{
    public class SimpleServiceLocator : ISimpleServiceLocator
    {
        public T Get<T>() where T : class => DependencyService.Get<T>();
    }
}