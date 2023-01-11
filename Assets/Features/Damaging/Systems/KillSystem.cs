using Core.Ecs;
using Core.Ecs.Reserved.Tags;
using Core.Infrastructure;
using Damaging.Components;
using DeathProcessing;

namespace Damaging.Systems
{
    public class KillSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public KillSystem(IWorld world) => this.world = world;

        public void OnUpdate()
        {
            var entities = this.world.Filter(typeof(Damageable), typeof(Mortal));
            foreach (var entity in entities)
            {
                if(this.world.HasComponent<Dead>(entity)) continue;
                ref var damageable = ref this.world.GetComponent<Damageable>(entity);
                if(damageable.ReceivedDamage > damageable.TolerableDamage) this.world.SetComponent<Dead>(entity);
            }
        }
    }
}