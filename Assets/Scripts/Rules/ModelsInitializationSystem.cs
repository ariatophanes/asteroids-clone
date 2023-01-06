using Core;
using EnemySpawn;

namespace Rules
{
    public class ModelsInitializationSystem : IGameSystem
    {
        private readonly IWorld world;
        private readonly GameConfiguration models;

        public ModelsInitializationSystem(IWorld world, GameConfiguration models)
        {
            this.world = world;
            this.models = models;
        }

        public void OnStart()
        {
            ref var enemySpawnModel = ref this.world.GetComponent<EnemySpawningModel>(this.world.NewEntity());
            enemySpawnModel = this.models.EnemySpawningModel;
        }

        public void OnUpdate() { }

        public void OnStop() { }
    }
}