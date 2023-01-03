using Core;

namespace Simulation
{
    public class SimulationInitializationSystem : IGameSystem
    {
        private readonly IModelsStorage modelsStorage;

        public SimulationInitializationSystem(IModelsStorage modelsStorage) => this.modelsStorage = modelsStorage;

        public void OnStart()
        {
            this.modelsStorage.AddModel("Simulation");
            this.modelsStorage.AddComponent<CoordinateSystem>("Simulation");
            this.modelsStorage.AddComponent<Time>("Simulation");
        }

        public void OnUpdate() { }

        public void OnStop() { }
    }
}