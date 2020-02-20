namespace FileSync.Client.Services
{
    public interface IDialogService 
    {
        string OpenFile();
        string SaveFile(string filename);
        bool ShowConfirmationDialog(string title, string message);
    }
}
