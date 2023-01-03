using Core;
using Simulation;

namespace EnemySpawn
{
    public class EnemySpawnSystem : IGameSystem
    {
        private readonly IEnemyModelFactory enemyModelFactory;
        private readonly IModelsStorage modelsStorage;
        private IEnemyTypePicker typePicker;

        public EnemySpawnSystem(IModelsStorage modelsStorage)
        {
            this.enemyModelFactory = new EnemyModelFactory(modelsStorage);
            this.modelsStorage = modelsStorage;
        }

        public void OnStart()
        {
            ref var spawnRules = ref this.modelsStorage.GetComponent<EnemySpawnRules>("Rules");
            this.typePicker = new EnemyTypePicker(spawnRules.Ratio, spawnRules.Interval);
        }

        public void OnUpdate()
        {
            ref var time = ref modelsStorage.GetComponent<Time>("Simulation");
            ref var enemyType = ref typePicker.GetEnemyType(time.Elapsed);

            if (enemyType != -1) enemyModelFactory.Create(enemyType);
        }

        public void OnStop() { }
    }
}