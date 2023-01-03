using System;

namespace Core
{
    public interface IModelsStorage : IDisposable
    {
        void AddModel(String name);
        void FreeModel(String name);
        ref T AddComponent<T>(String modelName) where T : unmanaged;
        ref T GetComponent<T>(String modelName) where T : unmanaged;
        bool HasModel(string modelName);
        bool HasComponent<T>(string modelName);
    }
}