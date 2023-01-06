using System;
using System.Collections.Generic;
using System.Linq;
using ModelName = System.String;

namespace Core
{
    public class World : IWorld
    {
        private readonly IDictionary<Type, IComponentsStorage> componentStorages;
        private readonly HashSet<int> liveEntities;
        private int generation;

        public World()
        {
            this.componentStorages = new Dictionary<Type, IComponentsStorage>(50);
            this.liveEntities = new HashSet<int>(100);
        }

        public void Dispose()
        {
            this.componentStorages.Clear();
            this.generation = 0;
        }

        public IEnumerable<int> Filter(params Type[] include)
        {
            var storages = include.Select(GetEntitiesOfType);
            var genesis = Array.Empty<int>().AsEnumerable();
            var including =
                include.Length == 1 ? this.componentStorages[include.First()]:
                storages.Aggregate(genesis, (prev, next) => prev.Intersect(next));

            return including;
        }

        public int NewEntity()
        {
            var ent = this.generation++;
            this.liveEntities.Add(ent);
            return ent;
        }

        public ref T GetComponent<T>(in int id) where T : unmanaged
        {
            var storage = GetComponentsStorage<T>();
            if (!storage.HasEntity(id)) storage.AddEntity(id);
            return ref storage.GetComponent(id);
        }

        private SparseComponentsStorage<T> GetComponentsStorage<T>() where T : unmanaged
        {
            var type = typeof(T);
            if (!this.componentStorages.ContainsKey(type)) this.componentStorages[type] = new SparseComponentsStorage<T>(capacity: 100);
            return (SparseComponentsStorage<T>) this.componentStorages[type];
        }

        private IEnumerable<int> GetEntitiesOfType(Type type)
        {
            if (!this.componentStorages.ContainsKey(type)) return Array.Empty<int>();
            return this.componentStorages[type];
        }

        public void DestroyEntity(int id)
        {
            this.liveEntities.Remove(id);
            var storages = this.componentStorages.Values.Where(s => s.HasEntity(id));
            foreach (var storage in storages) storage.RemoveEntity(id);
        }

        public bool HasEntity(in int entity) => this.liveEntities.Contains(entity);

        public void RemoveComponent<T>(in int id) where T : unmanaged => this.componentStorages[typeof(T)].RemoveEntity(id);
    }
}