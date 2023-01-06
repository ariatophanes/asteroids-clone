using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public readonly struct SparseComponentsStorage<T> : IComponentsStorage where T : unmanaged
    {
        private readonly SparseSet entities;
        private readonly T[] components;

        public SparseComponentsStorage(int capacity)
        {
            this.entities = new SparseSet(capacity);
            this.components = new T[capacity];
        }

        public void AddEntity(in int id) => this.entities.Add(id);

        public void RemoveEntity(in int id) => this.entities.Remove(id);

        public bool HasEntity(in int id) => this.entities.Contains(id);

        public ref T GetComponent(in int id) => ref this.components[this.entities.IndexOf(id)];
        
        IEnumerable<int> IComponentsStorage.EntitiesSet => this.entities;
        public IEnumerator<int> GetEnumerator() => this.entities.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}