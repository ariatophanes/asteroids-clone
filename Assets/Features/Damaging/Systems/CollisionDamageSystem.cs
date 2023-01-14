using Core.Ecs;
using Core.Infrastructure;
using Damaging.Components;
using DeathProcessing;
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

                foreach (var other in selfCollidedEntities.CollidedEntities)
                {
                    if (!this.world.HasComponent<Damage>(other)) continue;
                    if (!this.world.HasComponent<TeamMember>(other)) continue;

                    ref var otherTeam = ref this.world.GetComponent<TeamMember>(other);
                    ref var otherDamage = ref this.world.GetComponent<Damage>(other);

                    ApplyDamage(ref selfDamageable, otherTeam, selfTeam, otherDamage);
                }
            }
        }

        private static void ApplyDamage(ref Damageable damageable, in TeamMember otherTeam, in TeamMember selfTeam, in Damage damage)
        {
            if (otherTeam.Tag == selfTeam.Tag) return;
            damageable.ReceivedDamage += damage.Value;
        }
    }
}