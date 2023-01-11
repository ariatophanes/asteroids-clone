using System;
using System.Collections.Generic;

namespace Core.Infrastructure
{
    public interface IWorld : IDisposable
    {
        int NewEntity();
        ref T GetComponent<T>(in int id);
        void SetComponent<T>(in int id, T component = default);
        bool HasComponent<T>(in int id);
        void DestroyEntity(int id);
        void RemoveComponent<T>(in int id);
        IEnumerable<int> Filter(params Type[] include);
        bool HasEntity(int entity);
        bool IsAlive(in int entity);
    }
}