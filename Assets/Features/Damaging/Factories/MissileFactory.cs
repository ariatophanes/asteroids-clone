using System;
using Core.Infrastructure;
using Core.SharedTags;
using Core.Simulation.Common;
using Core.Simulation.Physics2D.Collisions;
using Core.Simulation.Physics2D.Extensions;
using Features.Damaging.Components;
using Features.Damaging.Presets;
using Features.MovementBehaviours.Forward;
using static Features.Damaging.Components.TeamMember;

namespace Features.Damaging.Factories
{
    public class AttackFactory : IAttackFactory
    {
        private readonly IWorld world;
        private readonly MissilePreset preset;
        private readonly IViewKernel viewsKernel;

        public AttackFactory(IWorld world, MissilePreset preset, IViewKernel viewsKernel)
        {
            this.viewsKernel = viewsKernel;
            this.preset = preset;
            this.world = world;
        }

        public void Create(Transform origin, float distance = 0.5f, TeamTag tag = TeamTag.Blue)
        {
            var missile = this.world.NewEntity();

            var rot = origin.Rotation;
            var posX = origin.Position.X + Math.Cos(origin.Rotation.ToRadians()) * distance;
            var posY = origin.Position.Y + Math.Sin(origin.Rotation.ToRadians()) * distance;

            this.world.SetComponent(missile, new Transform((float) posX, (float) posY, rot));
            this.world.SetComponent(missile, new TeamMember(tag));
            this.world.SetComponent(missile, new Collidable(5));
            this.world.SetComponent(missile, this.preset.Damage);
            this.world.SetComponent(missile, this.preset.Rb);
            this.world.SetComponent(missile, this.preset.Damageable);
            this.world.SetComponent(missile, this.preset.Radius);
            this.world.SetComponent<CircleCollider2D>(missile);
            this.world.SetComponent<Mortal>(missile);
            this.world.SetComponent<MoveForwardBehaviour>(missile);
            this.viewsKernel.BindView(missile, this.preset.ViewPath);
        }
    }

    public interface IAttackFactory
    {
        void Create(Transform origin, float distance = 0.5f, TeamTag tag = TeamTag.Blue);
    }
}