using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityAdaptation
{
    public class UnityViewKernel : IViewKernel
    {
        private readonly Dictionary<IView, GameObject> gameObjects;
        private readonly Dictionary<int, IView> views;
        private readonly HashSet<int> bindings, unresolvedBindings;
        private readonly IWorld world;
        private readonly IAssetProvider assetProvider;

        public UnityViewKernel(IWorld world, IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
            this.world = world;
            this.views = new Dictionary<int, IView>(100);
            this.gameObjects = new Dictionary<IView, GameObject>(100);
            this.bindings = new HashSet<int>(100);
            this.unresolvedBindings = new HashSet<int>(30);
        }

        public void BindView(int id, string path)
        {
            if (this.bindings.Contains(id)) throw new ArgumentException();

            var prefab = this.assetProvider.Load<GameObject>(path);
            var go = Object.Instantiate(prefab);
            var modelView = go.GetComponent<IView>();

            this.views[id] = modelView;
            this.gameObjects[modelView] = go;
            this.bindings.Add(id);
        }

        public void DestroyView(int id)
        {
            if (!this.bindings.Contains(id)) throw new ArgumentException();

            var modelView = this.views[id];
            var go = this.gameObjects[modelView];

            this.gameObjects.Remove(modelView);
            this.views.Remove(id);
            this.bindings.Remove(id);

            Object.Destroy(go);
        }

        public void Update()
        {
            foreach (var id in this.bindings.Where(IsBindingResolvable)) this.views[id].OnUpdate(id, this.world);
        }

        private bool IsBindingResolvable(int id)
        {
            if (this.world.HasEntity(id)) return true;
            if (this.unresolvedBindings.Contains(id)) return false;
            this.unresolvedBindings.Add(id);
            
            return false;
        }

        public void FixUnresolvedBindings()
        {
            foreach (var id in this.unresolvedBindings) DestroyView(id);
            this.unresolvedBindings.Clear();
        }
    }
}