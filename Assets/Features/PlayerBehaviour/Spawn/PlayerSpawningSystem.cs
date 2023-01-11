using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Tags;
using Core.ViewBindingAutomation;
using Damaging.Components;
using DeathProcessing;
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
        private readonly EntityPresets presets;

        public PlayerSpawningSystem(IWorld world, EntityPresets presets)
        {
            this.presets = presets;
            this.world = world;
        }

        public void OnStart()
        {
            var player = this.world.NewEntity();
            this.world.SetComponent(player, new TeamMember(Blue));
            this.world.SetComponent(player, new AutoViewBinding(presets.PlayerPreset.ViewPath));
            this.world.SetComponent(player, new Damage(1));
            this.world.SetComponent(player, new Damageable(1));
            this.world.SetComponent<Player>(player);
            this.world.SetComponent<Mortal>(player);
            this.world.SetComponent<CircleCollider2D>(player);
            this.world.SetComponent(player, presets.PlayerPreset.Radius);
            this.world.SetComponent(player, presets.PlayerPreset.Rb);

            var gamingFieldEnt = this.world.Filter(typeof(GamingField)).First();
            ref var transform = ref this.world.GetComponent<Transform>(player);
            ref var gamingField = ref this.world.GetComponent<GamingField>(gamingFieldEnt);
            transform.Position.X = (gamingField.BoundsHorizontal.X + gamingField.BoundsHorizontal.Y) / 2;
            transform.Position.Y = (gamingField.BoundsVertical.X + gamingField.BoundsVertical.Y) / 2;
        }
    }
}