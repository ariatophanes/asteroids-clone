using System.Collections.Generic;
using Core.Infrastructure;
using UnityAdaptation;

namespace Core.ViewBindingAutomation
{
    public class ViewKernel : IViewKernel
    {
        private readonly IWorld world;
        private readonly Dictionary<int, IView> map;
        private readonly IAssetProvider assetProvider;
        
        public ViewKernel(IWorld world, IAssetProvider assetProvider)
        {
            this.map = new Dictionary<int, IView>();
            this.assetProvider = assetProvider;
            this.world = world;
        }

        public void BindView(in int id, string path) => this.map[id] = this.assetProvider.InstantiateActor<IView>(path);

        public void UnbindView(in int id) => map.Remove(id);
        
        public void DestroyView(in int id) => map[id].DestroySelf();

        public void Update()
        {
            foreach (var (id, view) in map) view.OnUpdate(id, this.world);
        }
    }
}