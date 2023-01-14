using UnityEngine;

namespace UnityAdaptation
{
    public class UnityAssetProvider : IAssetProvider
    {
        public T InstantiateActor<T>(string path) => GameObject.Instantiate(Resources.Load<GameObject>(path)).GetComponent<T>();
        public T Load<T>(string path) where T : class => Resources.Load(path) as T;
    }
}