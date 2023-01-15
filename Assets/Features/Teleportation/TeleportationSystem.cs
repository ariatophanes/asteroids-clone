using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Simulation.Common;

namespace Features.Teleportation
{
    public class TeleportationSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public TeleportationSystem(IWorld world) => this.world = world;
        
        public void OnUpdate()
        {
            var fieldEnt = this.world.Filter(typeof(GamingField)).First();
            ref var field = ref this.world.GetComponent<GamingField>(fieldEnt);

            foreach (var entity in this.world.Filter(typeof(Transform), typeof(Radius), typeof(Teleportable)))
            {
                ref var transform = ref this.world.GetComponent<Transform>(entity);
                ref var radius = ref this.world.GetComponent<Radius>(entity);
                var offset = radius.Value / 4;

                if (transform.Position.X < field.BoundsHorizontal.X - offset) transform.Position.X = field.BoundsHorizontal.Y + offset;

                if (transform.Position.X > field.BoundsHorizontal.Y + offset) transform.Position.X = field.BoundsHorizontal.X - offset;

                if (transform.Position.Y < field.BoundsVertical.X - offset) transform.Position.Y = field.BoundsVertical.Y + offset;

                if (transform.Position.Y > field.BoundsVertical.Y + offset) transform.Position.Y = field.BoundsVertical.X - offset;


            }
        }
    }
}