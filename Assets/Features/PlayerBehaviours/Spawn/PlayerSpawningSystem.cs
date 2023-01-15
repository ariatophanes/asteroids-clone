using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.SharedTags;
using Core.Simulation.Common;
using Core.Simulation.Physics2D.Collisions;
using Features.Damaging.Components;
using Features.Damaging.Presets;
using Features.PlayerBehaviours.Presets;
using Features.Teleportation;
using static Features.Damaging.Components.TeamMember.TeamTag;

namespace Features.PlayerBehaviours.Spawn
{
    public class PlayerSpawningSystem : IStartCallbackReceiver
    {
        private readonly IWorld world;
        private readonly PlayerPreset playerPreset;
        private readonly IViewKernel viewKernel;
        private readonly AttackPresets attackPresets;

        public PlayerSpawningSystem(IWorld world, PlayerPreset playerPreset, AttackPresets attackPresets, IViewKernel viewKernel)
        {
            this.attackPresets = attackPresets;
            this.viewKernel = viewKernel;
            this.world = world;
            this.playerPreset = playerPreset;
        }

        public void OnStart()
        {
            var player = this.world.NewEntity();
            var gamingFieldEnt = this.world.Filter(typeof(GamingField)).First();

            this.world.SetComponent(player, new TeamMember(Blue));
            this.world.SetComponent(player, new Damage(1));
            this.world.SetComponent(player, new Damageable(1));
            this.world.SetComponent(player, new Collidable(5)); 
            this.world.SetComponent(player, new Fighter(2));
            this.world.SetComponent<Player>(player);
            this.world.SetComponent<Mortal>(player);
            this.world.SetComponent<Teleportable>(player);
            this.world.SetComponent<CircleCollider2D>(player);
            this.world.SetComponent(player, this.playerPreset.Radius);
            this.world.SetComponent(player, this.playerPreset.Rb);
            this.viewKernel.BindView(player, this.playerPreset.ViewPath);

            ref var transform = ref this.world.GetComponent<Transform>(player);
            ref var gamingField = ref this.world.GetComponent<GamingField>(gamingFieldEnt);

            transform.Position.X = (gamingField.BoundsHorizontal.X + gamingField.BoundsHorizontal.Y) / 2;
            transform.Position.Y = (gamingField.BoundsVertical.X + gamingField.BoundsVertical.Y) / 2;
            
            ref var fighter = ref this.world.GetComponent<Fighter>(player);
            fighter.AddAttack(this.attackPresets.FirstAttack, AttackType.Gun);
            fighter.AddAttack(this.attackPresets.SecondAttack, AttackType.Laser);
        }
    }
}