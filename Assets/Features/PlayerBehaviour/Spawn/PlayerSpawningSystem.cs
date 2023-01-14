using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Tags;
using Damaging.Components;
using DeathProcessing;
using Presets;
using Simulation.Common;
using Simulation.Physics2D;
using Simulation.Physics2D.Collisions;
using UnityAdaptation;
using static Damaging.Components.TeamMember.TeamTag;

namespace PlayerBehaviour.Spawn
{
    public class PlayerSpawningSystem : IStartCallbackReceiver
    {
        private readonly IWorld world;
        private readonly PlayerPreset preset;
        private readonly IViewKernel viewKernel;

        public PlayerSpawningSystem(IWorld world, EntityPresets presets, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
            this.preset = presets.PlayerPreset;
        }

        public void OnStart()
        {
            var player = this.world.NewEntity();
            var gamingFieldEnt = this.world.Filter(typeof(GamingField)).First();
            
            this.world.SetComponent(player, new TeamMember(Blue));
            this.world.SetComponent(player, new Damage(1));
            this.world.SetComponent(player, new Damageable(1));
            this.world.SetComponent(player, new Collidable(5));
            this.world.SetComponent<Player>(player);
            this.world.SetComponent<Mortal>(player);
            this.world.SetComponent<CircleCollider2D>(player);
            this.world.SetComponent(player, this.preset.Radius);
            this.world.SetComponent(player, this.preset.Rb);
            this.viewKernel.BindView(player, this.preset.ViewPath);

            ref var transform = ref this.world.GetComponent<Transform>(player);
            ref var gamingField = ref this.world.GetComponent<GamingField>(gamingFieldEnt);

            transform.Position.X = (gamingField.BoundsHorizontal.X + gamingField.BoundsHorizontal.Y) / 2;
            transform.Position.Y = (gamingField.BoundsVertical.X + gamingField.BoundsVertical.Y) / 2;
        }
    }
}