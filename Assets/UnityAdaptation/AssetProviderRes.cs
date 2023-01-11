using UnityEngine;

namespace UnityAdaptation
{
    public class AssetProviderRes : IAssetProvider
    {
        public T Load<T>(string path) where T : Object => Resources.Load<T>(path);
    }
}