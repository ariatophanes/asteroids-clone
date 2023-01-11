using UnityEngine;

namespace UnityAdaptation
{
    public interface IAssetProvider
    {
        public T Load<T>(string path) where T : Object;
    }
}