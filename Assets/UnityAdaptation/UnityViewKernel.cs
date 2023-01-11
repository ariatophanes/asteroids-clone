using System.Collections.Generic;
using Core.Infrastructure;
using UnityEngine;

namespace UnityAdaptation
{
    public class UnityViewKernel : IViewKernel
    {
        private readonly List<Binding> bindings;
        private readonly List<Binding> brokenBindings;
        private readonly IWorld world;
        private readonly IAssetProvider assetProvider;

        public UnityViewKernel(IWorld world, IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
            this.world = world;
            this.bindings = new List<Binding>();
            this.brokenBindings = new List<Binding>();
        }

        public void BindView(int id, string path)
        {
            var prefab = this.assetProvider.Load<GameObject>(path);
            var go = GameObject.Instantiate(prefab);
            var view = go.GetComponent<IView>();

            this.bindings.Add(new Binding(id, view));
        }

        public void Update()
        {
            foreach (var binding in bindings)
            {
                if (this.world.IsAlive(binding.EntityId)) continue;
                this.brokenBindings.Add(binding);
            }
            
            foreach (var binding in this.brokenBindings)
            {
                this.bindings.Remove(binding);
                binding.View.DestroySelf();
            }

            foreach (var binding in this.bindings) binding.View.OnUpdate(binding.EntityId, this.world);
            
            this.brokenBindings.Clear();
        }

        private struct Binding
        {
            public readonly int EntityId;
            public readonly IView View;

            public Binding(int entityId, IView view)
            {
                this.EntityId = entityId;
                this.View = view;
            }
        }
    }
}