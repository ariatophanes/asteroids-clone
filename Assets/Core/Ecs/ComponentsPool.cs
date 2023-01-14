using System.Collections;
using System.Collections.Generic;
using Core.Collections;

namespace Core.Ecs
{
    public readonly struct ComponentsPool<T> : IComponentsPool
    {
        private readonly SparseSet entityIndices;
        private readonly T[] components;

        public ComponentsPool(int capacity)
        {
            this.entityIndices = new SparseSet(capacity);
            this.components = new T[capacity];
        }

        public void AddEntity(in int id)
        {
            this.entityIndices.Add(id);
        }

        public void RemoveEntity(in int id)
        {
            var index = this.entityIndices.IndexOf(id);
            var lastIndex = this.entityIndices.Count - 1;
            (this.components[index], this.components[lastIndex]) = (this.components[lastIndex], this.components[index]);
            this.entityIndices.Remove(id);
        }

        public void SetComponent(in int id, T component) => this.components[this.entityIndices.IndexOf(id)] = component;

        public bool HasEntity(in int id) => this.entityIndices.Contains(id);

        public ref T GetComponent(in int id) => ref this.components[this.entityIndices.IndexOf(id)];

        public IEnumerator<int> GetEnumerator() => this.entityIndices.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}