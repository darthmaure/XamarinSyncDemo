namespace FileSync.Shared.Services
{
    public interface ISimpleServiceLocator
    {
        T Get<T>() where T: class;
    }
}
