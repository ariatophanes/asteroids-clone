using System;
using System.Collections.Generic;

namespace Core
{
    public interface IWorld : IDisposable
    {
        int NewEntity();
        ref T GetComponent<T>(in int id) where T : unmanaged;
        void DestroyEntity(int id);
        void RemoveComponent<T>(in int id) where T : unmanaged;
        IEnumerable<int> Filter(params Type[] include);
        bool HasEntity(in int entity);
    }
}