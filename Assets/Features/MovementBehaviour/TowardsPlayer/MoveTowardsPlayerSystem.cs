using System.Linq;
using System.Numerics;
using Core.Ecs;
using Core.Infrastructure;
using Core.Tags;
using Simulation.Physics2D;

namespace MovementBehaviour.TowardsPlayer
{
    public class MoveTowardsPlayerSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public MoveTowardsPlayerSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var player = this.world.Filter(typeof(Player)).FirstOrDefault();
            ref var playerTransform = ref this.world.GetComponent<Transform>(player);

            if (player < 0) return;

            var entities = this.world.Filter(new[]
            {
                typeof(MoveTowardsPlayerBehaviour),
                typeof(Transform),
                typeof(Rigidbody2D)
            });

            foreach (var entity in entities)
            {
                ref var transform = ref this.world.GetComponent<Transform>(entity);
                ref var rb = ref this.world.GetComponent<Rigidbody2D>(entity);

                var dir = Vector2.Normalize(playerTransform.Position - transform.Position);

                rb.LinearForce += dir * 10;
            }
        }
    }
}