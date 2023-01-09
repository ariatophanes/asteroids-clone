using System.Linq;
using Core;
using InputListener;
using Simulation;
using Simulation.Physics2D;
using Tags;

namespace PlayerBehaviour
{
    public class PlayerShootingSystem : IStartCallbackReceiver, IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;
        private IMissileFactory missileFactory;

        public PlayerShootingSystem(IWorld world, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
        }

        public void OnStart()
        {
            this.missileFactory = new MissileFactory(this.world, this.viewKernel);
        }

        public void OnUpdate()
        {
            var playerEnt = this.world.Filter(typeof(Player)).First();
            var inputEnt = this.world.Filter(typeof(InputFrame)).First();

            ref var input = ref this.world.GetComponent<InputFrame>(inputEnt);
            ref var transform = ref this.world.GetComponent<Transform>(playerEnt);
            ref var rad = ref this.world.GetComponent<Radius>(playerEnt);

            if (input.AttackType != AttackType.None) this.missileFactory.Create(input.AttackType, transform, rad.Value);
        }
    }
}