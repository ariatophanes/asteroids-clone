using System;
using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Tags;
using InputListening;
using Simulation.Physics2D;
using Simulation.Physics2D.Extensions;

namespace PlayerBehaviour.Movement
{
    public class PlayerMovementSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public PlayerMovementSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var playerEnt = this.world.Filter(typeof(Player)).FirstOrDefault();
            var inputEnt = this.world.Filter(typeof(InputFrame)).First();

            if (playerEnt < 0) return;

            ref var input = ref this.world.GetComponent<InputFrame>(inputEnt);
            ref var rb = ref this.world.GetComponent<Rigidbody2D>(playerEnt);
            ref var t = ref this.world.GetComponent<Transform>(playerEnt);

            var xAxis = input.HorizontalAxis;
            var yAxis = Math.Clamp(input.VerticalAxis, 0, 1);
            var xForce = (float) Math.Cos(t.Rotation.ToRadians()) * 10;
            var yForce = (float) Math.Sin(t.Rotation.ToRadians()) * 10;

            rb.AngularForce += xAxis * 10000;
            rb.LinearForce.X += yAxis * xForce;
            rb.LinearForce.Y += yAxis * yForce;
        }
    }
}