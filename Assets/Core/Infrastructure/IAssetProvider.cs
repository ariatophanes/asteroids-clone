namespace UnityAdaptation
{
    public interface IAssetProvider
    {
        T InstantiateActor<T>(string path);
        T Load<T>(string path) where T : class;
    }
}