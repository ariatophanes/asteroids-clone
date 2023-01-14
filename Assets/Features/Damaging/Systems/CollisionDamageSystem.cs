using Core.Ecs;
using Core.Infrastructure;
using Damaging.Components;
using DeathProcessing;
using Simulation.Physics2D;
using Simulation.Physics2D.Collisions;

namespace Damaging.Systems
{
    public class CollisionDamageSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public CollisionDamageSystem(IWorld world) => this.world = world;

        public void OnUpdate()
        {
            var entities = this.world.Filter(typeof(Damageable), typeof(TeamMember), typeof(Collidable), typeof(Mortal));

            foreach (var self in entities)
            {
                ref var selfCollidedEntities = ref this.world.GetComponent<Collidable>(self);
                ref var selfTeam = ref this.world.GetComponent<TeamMember>(self);
                ref var selfDamageable = ref this.world.GetComponent<Damageable>(self);
                ref var selfMortal = ref this.world.GetComponent<Mortal>(self);
                ref var selfRb = ref this.world.GetComponent<Rigidbody2D>(self);

                foreach (var other in selfCollidedEntities.CollidedEntities)
                {
                    if (!this.world.HasComponent<Damage>(other)) continue;
                    if (!this.world.HasComponent<TeamMember>(other)) continue;

                    ref var otherTeam = ref this.world.GetComponent<TeamMember>(other);
                    ref var otherDamage = ref this.world.GetComponent<Damage>(other);
                    ref var otherRb = ref this.world.GetComponent<Rigidbody2D>(other);

                    if (otherTeam.Tag == selfTeam.Tag) continue;

                    // ApplyForce(ref selfRb, ref otherRb);
                    ApplyDamage(ref selfMortal, ref selfDamageable, otherDamage);
                }
            }
        }

        private static void ApplyDamage(ref Mortal mortal, ref Damageable damageable, in Damage damage)
        {
            damageable.ReceivedDamage += damage.Value;
            if (damageable.ReceivedDamage > damageable.TolerableDamage) mortal.IsDead = true;
        }

        private static void ApplyForce(ref Rigidbody2D selfRb, ref Rigidbody2D otherRb)
        {
            otherRb.LinearForce += selfRb.LinearForce / otherRb.Mass;
        }
    }
}