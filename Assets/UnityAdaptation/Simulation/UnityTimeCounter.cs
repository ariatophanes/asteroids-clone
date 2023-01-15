using System.Linq;
using Core.Infrastructure;
using Core.Simulation.Common;

namespace UnityAdaptation.Simulation
{
    public class UnityTimeCounter : IUpdateCallbackReceiver, IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public UnityTimeCounter(IWorld world) => this.world = world;

        public void OnUpdate()
        {
            var simulation = this.world.Filter(typeof(Time)).First();
            ref var time = ref this.world.GetComponent<Time>(simulation);

            time.Elapsed = UnityEngine.Time.timeSinceLevelLoad;
            time.Delta = UnityEngine.Time.deltaTime;
        }

        public void OnFixedUpdate()
        {
            var simulation = this.world.Filter(typeof(Time)).First();
            ref var time = ref this.world.GetComponent<Time>(simulation);

            time.FixedDelta = UnityEngine.Time.fixedDeltaTime;
        }
    }
}