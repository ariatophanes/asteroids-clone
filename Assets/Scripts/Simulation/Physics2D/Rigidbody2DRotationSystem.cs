using System.Linq;
using Core;

namespace Simulation.Physics2D
{
    public class Rigidbody2DRotationSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public Rigidbody2DRotationSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var timeEnt = this.world.Filter(typeof(Time)).First();
            var bodies = this.world.Filter(typeof(Rigidbody2D), typeof(Transform), typeof(Radius));

            ref var time = ref this.world.GetComponent<Time>(timeEnt);

            foreach (var body in bodies)
            {
                ref var rb = ref this.world.GetComponent<Rigidbody2D>(body);
                ref var rad = ref this.world.GetComponent<Radius>(body);
                ref var t = ref this.world.GetComponent<Transform>(body);

                var a = rb.AngularForce / rb.Mass * rad.Value;
                var dt = time.FixedDelta;
                var s = a * dt * dt / 2 + rb.AngularMomentumSpeed * dt;

                t.Rotation += s;
                
                rb.AngularMomentumSpeed = s / dt * 0.7f;
                rb.AngularForce *= 0.7f;
            }
        }

        public void OnStart() { }

        public void OnStop() { }
    }
}