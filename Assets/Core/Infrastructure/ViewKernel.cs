using System.Collections.Generic;

namespace Core.Infrastructure
{
    public class ViewKernel : IViewKernel
    {
        private readonly IWorld world;
        private readonly Dictionary<int, List<IEntityView>> map;

        private readonly IAssetFactory assetFactory;

        public ViewKernel(IWorld world, IAssetFactory assetFactory)
        {
            this.map = new Dictionary<int, List<IEntityView>>();
            this.assetFactory = assetFactory;
            this.world = world;
        }

        public void BindView(in int id, string path)
        {
            if (!this.map.ContainsKey(id)) this.map.Add(id, new List<IEntityView>(10));
            this.map[id].AddRange(this.assetFactory.InstantiateView(path));
        }

        public void UnbindView(in int id)
        {
            this.map.Remove(id);
        }

        public void DestroyView(in int id)
        {
            foreach (var view in this.map[id]) view.DestroySelf();
        }

        public void Update()
        {
            foreach (var (id, views) in this.map)
            foreach (var view in views)
                view.OnUpdate(id, this.world);
        }
    }
}