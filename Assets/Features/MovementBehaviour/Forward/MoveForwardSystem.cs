using System;
using Core.Ecs;
using Core.Infrastructure;
using Simulation.Physics2D;
using Simulation.Physics2D.Extensions;

namespace MovementBehaviour.Forward
{
    public class MoveForwardSystem : IFixedUpdateCallbackReceiver
    {
        private const int Mul = 50;
        private readonly IWorld world;

        public MoveForwardSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var entities = this.world.Filter(new[]
            {
                typeof(MoveForwardBehaviour),
                typeof(Transform),
                typeof(Rigidbody2D)
            });

            foreach (var entity in entities)
            {
                ref var transform = ref this.world.GetComponent<Transform>(entity);
                ref var rb = ref this.world.GetComponent<Rigidbody2D>(entity);

                rb.LinearForce.X = (float) (Math.Cos(transform.Rotation.ToRadians()) * Mul);
                rb.LinearForce.Y = (float) (Math.Sin(transform.Rotation.ToRadians()) * Mul);
            }
        }
    }
}