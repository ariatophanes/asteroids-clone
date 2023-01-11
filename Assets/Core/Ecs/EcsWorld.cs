using System;
using System.Collections.Generic;
using System.Linq;
using Core.Ecs.Reserved.Tags;
using Core.Infrastructure;
using static System.Int16;

namespace Core.Ecs
{
    public class EcsWorld : IWorld
    {
        private readonly IDictionary<Type, IComponentsStorage> componentStorages;
        private int entityCounter;

        public EcsWorld()
        {
            this.componentStorages = new Dictionary<Type, IComponentsStorage>(capacity: MaxValue);
        }

        public void Dispose()
        {
            this.componentStorages.Clear();
            this.entityCounter = 0;
        }

        public IEnumerable<int> Filter(params Type[] include)
        {
            if (include.Length == 1)
            {
                var type = include.First();
                if (!this.componentStorages.ContainsKey(type)) return Array.Empty<int>();
                return this.componentStorages[type];
            }

            var storages = include.Select(GetEntitiesOfType);
            return storages.Aggregate((prev, next) => prev.Intersect(next));
        }

        public int NewEntity() => this.entityCounter++;

        public ref T GetComponent<T>(in int id)
        {
            var storage = GetComponentsStorage<T>();
            if (!storage.HasEntity(id)) storage.AddEntity(id);
            return ref storage.GetComponent(id);
        }

        public void SetComponent<T>(in int id, T component)
        {
            var storage = GetComponentsStorage<T>();
            if (!storage.HasEntity(id)) storage.AddEntity(id);
            storage.SetComponent(id, component);
        }

        private SparseComponentsStorage<T> GetComponentsStorage<T>()
        {
            var type = typeof(T);
            if (!this.componentStorages.ContainsKey(type)) this.componentStorages[type] = new SparseComponentsStorage<T>(capacity: MaxValue);
            return (SparseComponentsStorage<T>) this.componentStorages[type];
        }

        private IEnumerable<int> GetEntitiesOfType(Type type)
        {
            if (!this.componentStorages.ContainsKey(type)) return Array.Empty<int>();
            return this.componentStorages[type];
        }

        public void DestroyEntity(int id)
        {
            var storages = this.componentStorages.Values.Where(s => s.HasEntity(id));
            foreach (var storage in storages) storage.RemoveEntity(id);
        }

        public bool HasComponent<T>(in int id) => GetComponentsStorage<T>().HasEntity(id);

        public bool HasEntity(int entity) => this.componentStorages.Values.Any(t => t.Contains(entity));
        public bool IsAlive(in int entity) => !HasComponent<Dead>(entity);

        public void RemoveComponent<T>(in int id) => this.componentStorages[typeof(T)].RemoveEntity(id);
    }
}