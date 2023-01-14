using System.Collections.Generic;
using Core.Ecs;
using UnityAdaptation;

namespace Core.Infrastructure
{
    public class ViewKernel : IViewKernel
    {
        private readonly IWorld world;
        private readonly Dictionary<int, IEntityView[]> map;

        private readonly IActorFactory actorFactory;
        
        public ViewKernel(IWorld world, IActorFactory actorFactory)
        {
            this.map = new Dictionary<int, IEntityView[]>();
            this.actorFactory = actorFactory;
            this.world = world;
        }

        public void BindView(in int id, string path) => this.map[id] = this.actorFactory.InstantiateActor(path);

        public void UnbindView(in int id) => this.map.Remove(id);
        
        public void DestroyView(in int id)
        {
            foreach (var view in this.map[id]) view.DestroySelf();
        }

        public void Update()
        {
            foreach (var (id, views) in this.map)
            foreach (var view in views) view.OnUpdate(id, this.world);
        }
    }
}