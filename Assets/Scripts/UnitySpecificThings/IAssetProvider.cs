using UnityEngine;

public interface IAssetProvider
{
    public T Load<T>(string path) where T : Object;
}