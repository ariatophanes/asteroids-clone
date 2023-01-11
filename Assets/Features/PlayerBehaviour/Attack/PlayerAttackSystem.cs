using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Tags;
using InputListening;
using Simulation.Physics2D;

namespace PlayerBehaviour.Attack
{
    public class PlayerAttackSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly IMissileFactory missileFactory;
        private readonly AttackType attackType;

        public PlayerAttackSystem(IWorld world, IMissileFactory missileFactory, AttackType attackType)
        {
            this.attackType = attackType;
            this.world = world;
            this.missileFactory = missileFactory;
        }

        public void OnUpdate()
        {
            var playerEnt = this.world.Filter(typeof(Player)).FirstOrDefault();
            var inputEnt = this.world.Filter(typeof(InputFrame)).First();

            if (playerEnt <= 0) return;

            ref var input = ref this.world.GetComponent<InputFrame>(inputEnt);
            ref var transform = ref this.world.GetComponent<Transform>(playerEnt);
            ref var rad = ref this.world.GetComponent<Radius>(playerEnt);

            if (input.AttackType == this.attackType) this.missileFactory.Create(transform, rad.Value);
        }
    }
}