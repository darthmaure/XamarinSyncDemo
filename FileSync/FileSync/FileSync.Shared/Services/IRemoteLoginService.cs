using System.Threading.Tasks;

namespace FileSync.Shared.Services
{
    public interface IRemoteLoginService
    {
        Task<bool> LoginAsync(string mail, string password, string apiKey);
    }
}
