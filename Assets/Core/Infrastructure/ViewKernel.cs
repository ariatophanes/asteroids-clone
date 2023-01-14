using System.Collections.Generic;
using UnityAdaptation;

namespace Core.Infrastructure
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

        public void UnbindView(in int id) => this.map.Remove(id);
        
        public void DestroyView(in int id) => this.map[id].DestroySelf();

        public void Update()
        {
            foreach (var (id, view) in this.map) view.OnUpdate(id, this.world);
        }
    }
}