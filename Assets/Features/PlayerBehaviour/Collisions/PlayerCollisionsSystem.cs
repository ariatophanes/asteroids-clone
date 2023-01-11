using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Tags;
using Simulation.Physics2D.Collisions;

namespace PlayerBehaviour.Collisions
{
    public class PlayerCollisionsSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public PlayerCollisionsSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
        }
    }
}