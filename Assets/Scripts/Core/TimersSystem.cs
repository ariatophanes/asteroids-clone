using System.Linq;
using Simulation;

namespace Core
{
    public class TimersSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public TimersSystem(IWorld world) => this.world = world;

        public void OnUpdate()
        {
            var timeEntity = this.world.Filter(typeof(Time)).First();
            ref var time = ref this.world.GetComponent<Time>(timeEntity);

            var timerEntities = this.world.Filter(typeof(Timer));

            foreach (var timerEnt in timerEntities)
            {
                ref var timer = ref this.world.GetComponent<Timer>(timerEnt);
                
                if (!timer.IsActive) continue;
                
                timer.ElapsedTime += time.Delta;
                
                if (timer.ElapsedTime > timer.LifeTime) timer.Deactivate();
            }
        }
    }
}