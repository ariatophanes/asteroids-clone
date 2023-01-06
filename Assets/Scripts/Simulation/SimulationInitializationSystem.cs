using Core;

namespace Simulation
{
    public class SimulationInitializationSystem : IGameSystem
    {
        private readonly IWorld world;

        public SimulationInitializationSystem(IWorld world) => this.world = world;

        public void OnStart()
        {
            var simulation = this.world.NewEntity();
            ref var time = ref this.world.GetComponent<Time>(simulation);
        }

        public void OnUpdate() { }

        public void OnStop() { }
    }
}