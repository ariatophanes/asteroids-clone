using System;
using System.Linq;
using Core;
using MovementBehaviour;
using Presets;
using Simulation;
using Simulation.Physics2D;
using Weapons;

namespace PlayerBehaviour
{
    public class MissileFactory : IMissileFactory
    {
        private readonly IWorld world;
        private readonly MissilePreset[] presets;
        private readonly IViewKernel viewKernel;

        public MissileFactory(IWorld world, IViewKernel viewKernel)
        {
            var presetsEnt = world.Filter(typeof(MissilePreset[])).First();
            this.world = world;
            this.presets = this.world.GetComponent<MissilePreset[]>(presetsEnt);
            this.viewKernel = viewKernel;
        }

        public void Create(AttackType attackType, Transform origin, float distance)
        {
            var missile = this.world.NewEntity();
            var preset = this.presets.First(p => p.AttackType == attackType);

            var rot = origin.Rotation;
            var posX = origin.Position.X + Math.Cos(origin.Rotation.ToRadians()) * distance;
            var posY = origin.Position.Y + Math.Sin(origin.Rotation.ToRadians()) * distance;
            
            this.world.SetComponent(missile, preset.Damage);
            this.world.SetComponent(missile, new Transform((float) posX, (float) posY, rot));
            this.world.SetComponent(missile, preset.Rb);
            this.world.SetComponent(missile, preset.Radius);
            this.world.SetComponent<CircleCollider2D>(missile);
            this.world.SetComponent<MoveForwardBehaviour>(missile);
            
            this.viewKernel.BindView(missile, preset.ViewPath);
        }
    }

    public interface IMissileFactory
    {
        void Create(AttackType attackType, Transform origin, float distance);
    }
}