using System.Threading.Tasks;

namespace FileSync.Shared.Services
{
    public interface ILoginService
    {
        Task<bool> Login(string mail, string password, string apiKey);
        Task Logout();
        Task<bool> IsLoggedIn();
    }
}
