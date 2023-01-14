using System.Collections.Generic;

namespace Core.Ecs
{
    public interface IComponentsPool : IEnumerable<int>
    {
        void AddEntity(in int id);
        void RemoveEntity(in int id);
        bool HasEntity(in int id);
    }
}