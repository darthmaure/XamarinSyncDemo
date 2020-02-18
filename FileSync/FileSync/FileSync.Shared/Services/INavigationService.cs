using System.Threading.Tasks;

namespace FileSync.Shared.Services
{
    public interface INavigationService
    {
        Task NavigateHomeAsync();
        Task NavigateLoginAsync();
        Task NavigateAsync(string key);
    }
}
