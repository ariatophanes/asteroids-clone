using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe; //cant avoid 
using ModelName = System.String;
using ComponentList = System.Collections.Generic.Dictionary<System.Int32, System.IntPtr>;

namespace Core
{
    public class ModelsStorage : IModelsStorage
    {
        private readonly IDictionary<ModelName, ComponentList> components;
        private readonly HashSet<string> modelNames;

        public ModelsStorage()
        {
            this.modelNames = new HashSet<string>(100);
            this.components = new Dictionary<ModelName, ComponentList>(100);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddModel(ModelName name)
        {
            if (HasModel(name)) throw new ArgumentException();

            string.Intern(name);

            this.components[name] = new ComponentList();
            this.modelNames.Add(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FreeModel(ModelName name)
        {
            foreach (var component in this.components[name].Keys.ToList()) DetachComponentInternal(name, component);

            this.components.Remove(name);
            this.modelNames.Remove(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe ref T AddComponent<T>(ModelName modelName) where T : unmanaged
        {
            if (!HasModel(modelName)) throw new ArgumentException();
            if (HasComponent(modelName, ComponentMeta<T>.Id)) throw new ArgumentException();

            var ptr = Marshal.AllocHGlobal(sizeof(T));
            Marshal.StructureToPtr(default(T), ptr, true);

            this.components[modelName][ComponentMeta<T>.Id] = ptr;

            return ref UnsafeUtility.AsRef<T>((void*) ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe ref T GetComponent<T>(ModelName modelName) where T : unmanaged
        {
            if (!HasModel(modelName)) throw new ArgumentException();
            if (!HasComponent(modelName, ComponentMeta<T>.Id)) throw new ArgumentException();
            
            var ptr = this.components[modelName][ComponentMeta<T>.Id];
            return ref UnsafeUtility.AsRef<T>((void*) ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DetachComponentInternal(ModelName modelName, int componentId)
        {
            if (!HasModel(modelName)) throw new ArgumentException();
            if (!HasComponent(modelName, componentId)) throw new ArgumentException();

            var ptr = this.components[modelName][componentId];
            Marshal.FreeHGlobal(ptr);

            this.components[modelName].Remove(componentId);
        }

        public void Dispose()
        {
            foreach (var model in this.components.Keys.ToList()) FreeModel(model);
        }

        public void FreeComponent<T>(ModelName modelName) where T : unmanaged => DetachComponentInternal(modelName, ComponentMeta<T>.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasModel(string modelName) => this.modelNames.Contains(modelName);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool HasComponent(string modelName, in int componentId) => this.components[modelName].ContainsKey(componentId);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>(string modelName) => HasComponent(modelName, ComponentMeta<T>.Id);
    }
}