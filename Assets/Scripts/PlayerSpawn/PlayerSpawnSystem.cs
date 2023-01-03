using Core;
using Simulation;

namespace PlayerSpawn
{
    public class PlayerSpawnSystem : IGameSystem
    {
        private readonly IModelsStorage modelsStorage;
        private readonly IViewKernel viewKernel;

        public PlayerSpawnSystem(IModelsStorage modelsStorage, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.modelsStorage = modelsStorage;
        }

        public void OnStart()
        {
            var modelName = $"Player#1";
            this.modelsStorage.AddModel(modelName);

            ref var coordSystem = ref this.modelsStorage.GetComponent<CoordinateSystem>("Simulation");
            ref var transform = ref this.modelsStorage.AddComponent<Transform>(modelName);
            
            (transform.Position.X, transform.Position.Y) = (coordSystem.Center.X, coordSystem.Center.Y);
            (transform.Rotation.X, transform.Rotation.Y, transform.Rotation.Z) = (0, 0, 0);

            this.viewKernel.InstantiateModelView(modelName, "Views/Player");
        }

        public void OnUpdate()
        {
            ref var transform = ref this.modelsStorage.GetComponent<Transform>($"Player#1");
            transform.Position.Y += 0.015f;
        }

        public void OnStop() { }
    }
}