using System;
using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure;
using static System.Int16;

namespace Core.Ecs
{
    public class EcsWorld : IWorld
    {
        private readonly IDictionary<Type, IComponentsPool> componentStorages;
        private int entityCounter;

        public EcsWorld() => this.componentStorages = new Dictionary<Type, IComponentsPool>(capacity: MaxValue);

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

        private ComponentsPool<T> GetComponentsStorage<T>()
        {
            var type = typeof(T);
            if (!this.componentStorages.ContainsKey(type)) this.componentStorages[type] = new ComponentsPool<T>(capacity: MaxValue);
            return (ComponentsPool<T>) this.componentStorages[type];
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

        public bool HasEntity(int entity) => this.componentStorages.Any(c => c.Value.HasEntity(entity));

        public void RemoveComponent<T>(in int id) => this.componentStorages[typeof(T)].RemoveEntity(id);

        public IEnumerable<int> Filter(params Type[] include)
        {
            if (include.Length == 1)
            {
                var type = include.First();
                if (!this.componentStorages.ContainsKey(type)) return Array.Empty<int>();
                return this.componentStorages[type].ToList();
            }

            var storages = include.Select(GetEntitiesOfType);
            return storages.Aggregate((prev, next) => prev.Intersect(next));
        }

        public void Dispose()
        {
            this.componentStorages.Clear();
            this.entityCounter = 0;
        }
    }
}