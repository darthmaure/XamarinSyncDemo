using System;
using System.Threading.Tasks;
using Firebase.Auth;
using System.Diagnostics;

namespace FileSync.Shared.Services
{
    public class RemoteLoginService : IRemoteLoginService
    {
        public async Task<bool> LoginAsync(string mail, string password, string apiKey)
        {
            try
            {
                var provider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var authLink = await provider.SignInWithEmailAndPasswordAsync(mail, password);

                return authLink?.FirebaseToken != null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }
}
