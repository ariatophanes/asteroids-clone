using Core;
using EnemySpawn;

namespace Rules
{
    public class RulesInitializationSystem : IGameSystem
    {
        private readonly IModelsStorage modelsStorage;
        private readonly GameConfiguration gameConfiguration;

        public RulesInitializationSystem(IModelsStorage modelsStorage, GameConfiguration gameConfiguration)
        {
            this.modelsStorage = modelsStorage;
            this.gameConfiguration = gameConfiguration;
        }

        public void OnStart()
        {
            this.modelsStorage.AddModel("Rules");

            ref var enemySpawnRules = ref this.modelsStorage.AddComponent<EnemySpawnRules>("Rules");
            enemySpawnRules.Interval = this.gameConfiguration.EnemiesSpawnInterval;
            enemySpawnRules.Ratio = this.gameConfiguration.EnemiesSpawnRatio;
        }

        public void OnUpdate() { }

        public void OnStop() { }
    }
}