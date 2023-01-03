using Core;
using Simulation;

namespace EnemySpawn
{
    public class EnemyModelFactory : IEnemyModelFactory
    {
        private readonly IModelsStorage modelsStorage;
        private int counter;

        public EnemyModelFactory(IModelsStorage modelsStorage) => this.modelsStorage = modelsStorage;

        public string Create(int type)
        {
            var modelName = $"Enemy#{this.counter++}";
            this.modelsStorage.AddModel(modelName);

            ref var coordSystem = ref this.modelsStorage.GetComponent<CoordinateSystem>("World");
            ref var t = ref this.modelsStorage.AddComponent<Transform>(modelName);

            (t.Position.X, t.Position.Y) = (coordSystem.Center.X, coordSystem.Center.Y);
            (t.Rotation.X, t.Rotation.Y, t.Rotation.Z) = (0, 0, 0);

            return modelName;
        }
    }
}