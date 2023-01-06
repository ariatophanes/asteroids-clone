
using System.Collections.Generic;

namespace Core
{
    public interface IComponentsStorage : IEnumerable<int>
    {
        public IEnumerable<int> EntitiesSet { get; }
        void AddEntity(in int id);
        void RemoveEntity(in int id);
        bool HasEntity(in int id);
    }
}