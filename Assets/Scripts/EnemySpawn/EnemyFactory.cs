using Core;
using Simulation;

namespace EnemySpawn
{
    public class EnemyFactory : IEnemyModelFactory
    {
        private readonly IWorld world;
        private int counter;

        public EnemyFactory(IWorld world) => this.world = world;

        public int Create(int type)
        {
            var enemy = this.world.NewEntity();

            ref var transform = ref this.world.GetComponent<Transform>(enemy);

            return enemy;
        }
    }
}