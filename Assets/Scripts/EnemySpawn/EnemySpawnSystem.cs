using System.Linq;
using Core;
using Simulation;

namespace EnemySpawn
{
    public class EnemySpawnSystem : IGameSystem
    {
        private readonly IEnemyModelFactory enemyModelFactory;
        private readonly IWorld world;
        private IEnemyTypePicker typePicker;

        public EnemySpawnSystem(IWorld world)
        {
            this.enemyModelFactory = new EnemyFactory(world);
            this.world = world;
        }

        public void OnStart()
        {
            // var modelEnt = this.world.Filter(typeof(EnemySpawningModel));
            // ref var model = ref this.world.GetComponent<EnemySpawningModel>(modelEnt.First());
            // this.typePicker = new EnemyTypePicker(model.Ratio, model.Interval);
        }

        public void OnUpdate()
        {
            // var timeEnt = this.world.Filter(typeof(Time));
            // ref var time = ref this.world.GetComponent<Time>(timeEnt.First());
            //
            // ref var enemyType = ref typePicker.GetEnemyType(time.Elapsed);
            //
            // if (enemyType != -1) enemyModelFactory.Create(enemyType);
        }

        public void OnStop() { }
    }
}