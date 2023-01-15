using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Simulation.Common;
using Features.Damaging.Components;

namespace Features.Damaging.Systems
{
    public class ReloadingSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public ReloadingSystem(IWorld world) => this.world = world;

        public void OnUpdate()
        {
            var fighterEntities = this.world.Filter(typeof(Fighter));
            var timeEntity = this.world.Filter(typeof(Time)).First();
            ref var time = ref this.world.GetComponent<Time>(timeEntity);
            
            foreach (var entity in fighterEntities)
            {
                ref var fighter = ref this.world.GetComponent<Fighter>(entity);
                var attackTypes = fighter.GetAllAttackTypes();
                
                foreach (var attackType in attackTypes)
                {
                    ref var attack = ref fighter.GetAttack(attackType);
                    if (attack.AmmoMagazine == attack.AmmoMagazineFull) continue;
                    attack.CurrentReloadingTime += time.Delta;
                    
                    if(attack.CurrentReloadingTime < attack.ReloadingTime) continue;
                    attack.CurrentReloadingTime = 0;
                    attack.AmmoMagazine++;
                }
            }
        }
    }
}