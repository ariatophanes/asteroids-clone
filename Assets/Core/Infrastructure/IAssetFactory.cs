namespace Core.Infrastructure
{
    public interface IAssetFactory
    {
        IEntityView[] InstantiateView(string path);
        T Load<T>(string path) where T : class;
        void UnloadUnused();
    }
}