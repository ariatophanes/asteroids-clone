using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Damaging.Components;
using Simulation.Physics2D.Collisions;

namespace Damaging.Systems
{
    public class CollisionDamageSystem : IFixedUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public CollisionDamageSystem(IWorld world) => this.world = world;

        public void OnFixedUpdate()
        {
            var damagers = this.world.Filter(typeof(CollidedEntities), typeof(Damage));
            var damageables = this.world.Filter(typeof(CollidedEntities), typeof(Damageable)).ToHashSet();

            foreach (var self in damagers)
            {
                ref var selfCollidedEntities = ref this.world.GetComponent<CollidedEntities>(self);
                ref var selfDamage = ref this.world.GetComponent<Damage>(self);
                ref var selfTeam = ref this.world.GetComponent<TeamMember>(self);

                var collided = selfCollidedEntities.Entities.Where(col => damageables.Contains(col));

                foreach (var other in collided)
                {
                    ref var otherTeam = ref this.world.GetComponent<TeamMember>(other);
                    ref var otherDamageable = ref this.world.GetComponent<Damageable>(other);
                    
                    if (selfTeam.Team == otherTeam.Team) continue;
                    
                    otherDamageable.ReceivedDamage += selfDamage.Value;
                }
            }
        }
    }
}