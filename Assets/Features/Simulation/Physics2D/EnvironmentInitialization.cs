using Core.Ecs;
using Core.Infrastructure;

namespace Simulation.Physics2D
{
    public class EnvironmentInitialization : IStartCallbackReceiver
    {
        private readonly IWorld world;

        public EnvironmentInitialization(IWorld world) => this.world = world;

        public void OnStart()
        {
            var simulation = this.world.NewEntity();
            this.world.GetComponent<Time>(simulation);
        }
    }
}