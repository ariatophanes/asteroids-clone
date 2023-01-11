using System;
using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.Random;
using Core.Tags;
using Core.ViewBindingAutomation;
using Damaging.Components;
using DeathProcessing;
using Presets;
using Simulation.Common;
using Simulation.Physics2D;
using Simulation.Physics2D.Collisions;
using static Damaging.Components.TeamMember.TeamTag;

namespace EnemySpawning
{
    public abstract class EnemyFactory
    {
        private readonly IWorld world;
        private readonly EnemyPreset preset;
        private readonly IRandom random;

        protected EnemyFactory(IWorld world, EnemyPreset preset, IRandom random)
        {
            this.random = random;
            this.world = world;
            this.preset = preset;
        }

        public virtual int Create()
        {
            var enemy = this.world.NewEntity();
            var boundsEnt = this.world.Filter(typeof(GamingField)).First();

            ref var radius = ref this.world.GetComponent<Radius>(enemy);
            ref var bounds = ref this.world.GetComponent<GamingField>(boundsEnt);

            var startPosX = this.random.Next((int) bounds.BoundsHorizontal.X, (int) bounds.BoundsHorizontal.Y);
            var startPosY = this.random.Next(0f, 1f) > 0.5f ? bounds.BoundsVertical.X - radius.Value : bounds.BoundsVertical.Y + radius.Value;

            this.world.SetComponent(enemy, new Transform(startPosX, startPosY, rotation: this.random.Next(-180, 180)));
            this.world.SetComponent(enemy, new AutoViewBinding(this.preset.ViewPath));
            this.world.SetComponent(enemy, new TeamMember(Red));
            this.world.SetComponent(enemy, new Damage(1));
            this.world.SetComponent<CircleCollider2D>(enemy);
            this.world.SetComponent<Mortal>(enemy);
            this.world.SetComponent<Enemy>(enemy);
            this.world.SetComponent(enemy, this.preset.Rb);
            this.world.SetComponent(enemy, this.preset.Radius);
            this.world.SetComponent(enemy, this.preset.Damageable);

            return enemy;
        }
    }
}