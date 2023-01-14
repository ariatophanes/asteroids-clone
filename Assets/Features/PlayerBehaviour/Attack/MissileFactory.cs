using System;
using Core.Infrastructure;
using Damaging.Components;
using DeathProcessing;
using MovementBehaviour.Forward;
using Presets;
using Simulation.Physics2D;
using Simulation.Physics2D.Collisions;
using Simulation.Physics2D.Extensions;
using static Damaging.Components.TeamMember;

namespace PlayerBehaviour.Attack
{
    public class MissileFactory : IMissileFactory
    {
        private readonly IWorld world;
        private readonly MissilePreset preset;
        private readonly TeamTag teamTag;
        private readonly IViewKernel viewsKernel;

        public MissileFactory(IWorld world, MissilePreset preset, IViewKernel viewsKernel, TeamTag teamTag = TeamTag.Blue)
        {
            this.viewsKernel = viewsKernel;
            this.teamTag = teamTag;
            this.preset = preset;
            this.world = world;
        }

        public void Create(Transform origin, float distance = 0.5f)
        {
            var missile = this.world.NewEntity();

            var rot = origin.Rotation;
            var posX = origin.Position.X + Math.Cos(origin.Rotation.ToRadians()) * distance;
            var posY = origin.Position.Y + Math.Sin(origin.Rotation.ToRadians()) * distance;

            this.world.SetComponent(missile, new Transform((float) posX, (float) posY, rot));
            this.world.SetComponent(missile, new TeamMember(this.teamTag));
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

    public interface IMissileFactory
    {
        void Create(Transform origin, float distance);
    }
}