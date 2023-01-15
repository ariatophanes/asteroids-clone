using Core.Infrastructure;
using UnityEngine;

namespace UnityAdaptation
{
    public class UnityAssetFactory : IAssetFactory
    {
        //todo: add pooling
        public IEntityView[] InstantiateView(string path) => GameObject.Instantiate(Resources.Load<GameObject>(path)).GetComponentsInChildren<IEntityView>();
        public T Load<T>(string path) where T : class => Resources.Load(path) as T;
        public void UnloadUnused() => Resources.UnloadUnusedAssets();
    }
}