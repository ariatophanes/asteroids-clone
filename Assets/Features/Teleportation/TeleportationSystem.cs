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

            foreach (var entity in this.world.Filter(typeof(Transform), typeof(Teleportable)))
            {
                ref var transform = ref this.world.GetComponent<Transform>(entity);

                if (transform.Position.X < field.BoundsHorizontal.X) transform.Position.X = field.BoundsHorizontal.Y - 1;

                if (transform.Position.X > field.BoundsHorizontal.Y) transform.Position.X = field.BoundsHorizontal.X + 1;

                if (transform.Position.Y < field.BoundsVertical.X) transform.Position.Y = field.BoundsVertical.Y - 5;

                if (transform.Position.Y > field.BoundsVertical.Y) transform.Position.Y = field.BoundsVertical.X + 5;
            }
        }
    }
}