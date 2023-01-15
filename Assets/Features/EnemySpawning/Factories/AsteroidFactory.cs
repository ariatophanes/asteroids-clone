using Core.Infrastructure;
using Core.Random;
using Features.EnemySpawning.Presets;
using Features.EnemySpawning.Tags;
using Features.MovementBehaviours.Forward;

namespace Features.EnemySpawning.Factories
{
    public class AsteroidFactory : EnemyFactory
    {
        private readonly IWorld world;

        public AsteroidFactory(IWorld world, EnemyPreset preset, IRandom random, IViewKernel viewKernel) : base(world, preset, random, viewKernel) =>
            this.world = world;

        public override int Create()
        {
            var asteroid = base.Create();
            this.world.SetComponent<MoveForwardBehaviour>(asteroid);
            this.world.SetComponent<Asteroid>(asteroid);
            return asteroid;
        }
    }
}