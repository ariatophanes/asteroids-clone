using Core.Ecs;
using Core.Infrastructure;
using Core.Timers;

namespace EnemySpawning
{
    public sealed class EnemySpawnSystem : IStartCallbackReceiver, IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly EnemyFactory enemyFactory;
        private readonly float interval;
        private int timerEntity;

        public EnemySpawnSystem(EnemyFactory enemyFactory, IWorld world, float interval = 3)
        {
            this.interval = interval;
            this.enemyFactory = enemyFactory;
            this.world = world;
        }

        public void OnStart()
        {
            this.timerEntity = this.world.NewEntity();
            ref var timer = ref this.world.GetComponent<Timer>(this.timerEntity);
            timer.Activate(this.interval);
        }

        public void OnUpdate()
        {
            ref var timer = ref this.world.GetComponent<Timer>(this.timerEntity);
            if (timer.IsActive) return;
            SpawnEnemy();
            timer.Restart();
        }
        
        private void SpawnEnemy() => this.enemyFactory.Create();
    }
}