using System;
using System.Linq;
using System.Numerics;
using Core;
using Simulation;
using Simulation.Physics2D;
using Tags;

namespace MovementBehaviour
{
    public class MoveToPlayerSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public MoveToPlayerSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var player = this.world.Filter(typeof(Player)).First();
            ref var playerTransform = ref this.world.GetComponent<Transform>(player);
            
            var entities = this.world.Filter(new[]
            {
                typeof(MoveToPlayerBehaviour),
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