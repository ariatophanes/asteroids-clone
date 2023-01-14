using Core.Infrastructure;
using Core.Random;
using Core.ViewBindingAutomation;
using EnemySpawning.Tags;
using MovementBehaviour.TowardsPlayer;
using Presets;

namespace EnemySpawning
{
    public class UfoFactory : EnemyFactory
    {
        private readonly IWorld world;

        public UfoFactory(IWorld world, EnemyPreset preset, IRandom random, IViewKernel viewKernel) : base(world, preset, random, viewKernel) =>
            this.world = world;

        public override int Create()
        {
            var ufo = base.Create();
            this.world.SetComponent<MoveTowardsPlayerBehaviour>(ufo);
            this.world.SetComponent<Ufo>(ufo);
            return ufo;
        }
    }
}