using System.Linq;
using Core;
using Core.Ecs;
using Core.Infrastructure;
using Simulation;
using Simulation.Common;
using Simulation.Physics2D;

namespace Teleportation
{
    public class TeleportationSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public TeleportationSystem(IWorld world) => this.world = world;
        
        public void OnUpdate()
        {
            var screenBordersEnt = this.world.Filter(typeof(GamingField)).First();
            ref var screen = ref this.world.GetComponent<GamingField>(screenBordersEnt);

            foreach (var entity in this.world.Filter(typeof(Transform), typeof(Radius)))
            {
                ref var transform = ref this.world.GetComponent<Transform>(entity);
                ref var radius = ref this.world.GetComponent<Radius>(entity);

                if (transform.Position.X < screen.BoundsHorizontal.X - radius.Value)
                    transform.Position.X = screen.BoundsHorizontal.Y + radius.Value;

                if (transform.Position.X > screen.BoundsHorizontal.Y + radius.Value)
                    transform.Position.X = screen.BoundsHorizontal.X - radius.Value;

                if (transform.Position.Y < screen.BoundsVertical.X - radius.Value)
                    transform.Position.Y = screen.BoundsVertical.Y + radius.Value;

                if (transform.Position.Y > screen.BoundsVertical.Y + radius.Value)
                    transform.Position.Y = screen.BoundsVertical.X - radius.Value;
            }
        }
    }
}