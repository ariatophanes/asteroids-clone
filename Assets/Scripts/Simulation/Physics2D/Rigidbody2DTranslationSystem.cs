using System.Linq;
using Core;

namespace Simulation.Physics2D
{
    public class Rigidbody2DTranslationSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public Rigidbody2DTranslationSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var timeEnt = this.world.Filter(typeof(Time)).First();
            var bodies = this.world.Filter(typeof(Rigidbody2D), typeof(Transform));

            ref var time = ref this.world.GetComponent<Time>(timeEnt);

            foreach (var body in bodies)
            {
                ref var rb = ref this.world.GetComponent<Rigidbody2D>(body);
                ref var t = ref this.world.GetComponent<Transform>(body);

                rb.LinearForce *= 1f - rb.Deceleration;
                
                var dt = time.FixedDelta;
                var s = rb.LinearForce / rb.Mass * (dt * dt) / 2 + rb.LinearMomentumSpeed * dt;

                t.Position += s;
                rb.LinearMomentumSpeed = s / dt / 2f;
            }
        }

        public void OnStart() { }

        public void OnStop() { }
    }
}