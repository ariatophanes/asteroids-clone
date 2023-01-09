using System;
using System.Linq;
using Core;
using MovementBehaviour;
using Simulation;

namespace EnemySpawning
{
    public class EnemySpawnSystem : IStartCallbackReceiver, IUpdateCallbackReceiver
    {
        private const float Ratio = 0.7f, Interval = 3;
        private readonly int timerEntity;
        private readonly IWorld world;
        private readonly IEnemyTypePicker typePicker;
        private readonly IViewKernel viewKernel;
        private IEnemyFactory enemyFactory;

        public enum EnemyType
        {
            Asteroid,
            UFO
        }

        public EnemySpawnSystem(IWorld world, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
            this.typePicker = new EnemyPickerByRatio(Ratio);

            this.timerEntity = this.world.NewEntity();
        }

        public void OnStart()
        {
            this.enemyFactory = new EnemyFactory(this.world, this.viewKernel);
            ref var timer = ref this.world.GetComponent<Timer>(timerEntity);
            timer.Activate(Interval);
        }

        public void OnUpdate()
        {
            ref var timer = ref this.world.GetComponent<Timer>(this.timerEntity);
            if (timer.IsActive) return;
            SpawnEnemy();
            timer.Restart();
        }
        
        private void SpawnEnemy()
        {
            var enemyType = this.typePicker.Next();
            var enemyEntity = this.enemyFactory.Create(enemyType);
            var timeEntity = this.world.Filter(typeof(Time)).First();

            ref var transform = ref this.world.GetComponent<Transform>(enemyEntity);
            ref var time = ref this.world.GetComponent<Time>(timeEntity);
            
            var rnd = new Random((int) time.Elapsed);
            
            transform.Rotation = (float) ((rnd.NextDouble() - 0.5) * 360);

            switch (enemyType)
            {
                case EnemyType.Asteroid:
                    this.world.SetComponent<MoveForwardBehaviour>(enemyEntity);
                    break;
                case EnemyType.UFO:
                    this.world.SetComponent<MoveToPlayerBehaviour>(enemyEntity);
                    break;
            }
        }
    }
}