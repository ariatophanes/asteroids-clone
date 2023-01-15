using System.Collections.Generic;
using Core.Ecs;
using Core.Infrastructure;
using Core.SharedTags;
using Core.Simulation.Common;
using Features.Damaging.Components;
using Features.Damaging.Factories;
using Features.Damaging.Presets;
using static Features.Damaging.Components.TeamMember.TeamTag;
using static Features.Damaging.Components.AttackType;

namespace Features.Damaging.Systems
{
    public class AttackSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly Dictionary<int, IAttackFactory> attackFactories;

        public AttackSystem(IWorld world, IViewKernel viewKernel, MissilePresets presets)
        {
            this.world = world;
            this.attackFactories = new Dictionary<int, IAttackFactory>
            {
                {Gun, new AttackFactory(world, presets.Gun, viewKernel)},
                {Laser, new AttackFactory(world, presets.Laser, viewKernel)}
            };
        }

        public void OnUpdate()
        {
            var fighters = this.world.Filter(typeof(Fighter));

            foreach (var entity in fighters)
            {
                ref var fighter = ref this.world.GetComponent<Fighter>(entity);
                var attackType = fighter.GetEmittingAttackType();
                
                if(attackType == None) continue;
                
                ref var attack = ref fighter.GetAttack(attackType);

                if (!this.attackFactories.ContainsKey(attackType)) continue;
                if (attack.AmmoMagazine == 0) continue;

                ref var transform = ref this.world.GetComponent<Transform>(entity);
                ref var r = ref this.world.GetComponent<Radius>(entity);
                var teamTag = this.world.HasComponent<Player>(entity) ? Blue : Red;

                this.attackFactories[attackType].Create(transform, distance: r.Value, tag: teamTag);
                attack.AmmoMagazine -= 1;
            }
        }
    }
}