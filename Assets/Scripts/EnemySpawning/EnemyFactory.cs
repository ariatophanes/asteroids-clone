using System;
using System.Linq;
using System.Numerics;
using Core;
using Presets;
using Simulation;
using Simulation.Physics2D;
using static EnemySpawning.EnemySpawnSystem;

namespace EnemySpawning
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IWorld world;
        private readonly EnemyPreset[] presets;
        private readonly GamingFieldBounds bounds;
        private readonly IViewKernel viewKernel;

        public EnemyFactory(IWorld world, IViewKernel viewKernel)
        {
            var presetsEnt = world.Filter(typeof(MissilePreset[])).First();
            var screenEnt = world.Filter(typeof(GamingFieldBounds)).First();

            this.bounds = world.GetComponent<GamingFieldBounds>(screenEnt);
            this.presets = world.GetComponent<EnemyPreset[]>(presetsEnt);
            this.world = world;
            this.viewKernel = viewKernel;
        }

        public int Create(EnemyType enemyType)
        {
            var timeEnt = this.world.Filter(typeof(Time)).First();
            var preset = this.presets.First(p => p.Type == enemyType);
            var enemy = this.world.NewEntity();

            ref var radius = ref this.world.GetComponent<Radius>(enemy);
            ref var time = ref this.world.GetComponent<Time>(timeEnt);

            var rnd = new Random(((int) time.Elapsed * 100));

            var startPosX = rnd.Next((int) this.bounds.BoundsHorizontal.X, (int) this.bounds.BoundsHorizontal.Y);

            var startPosY = rnd.NextDouble() > 0.5f
                ? this.bounds.BoundsVertical.X - radius.Value
                : this.bounds.BoundsVertical.Y + radius.Value;

            this.world.SetComponent(enemy, new Transform(startPosX, startPosY, rotation: rnd.Next(-180, 180)));
            this.world.SetComponent(enemy, preset.Rb);
            this.world.SetComponent(enemy, preset.Radius);
            this.world.SetComponent<CircleCollider2D>(enemy);
            this.viewKernel.BindView(enemy, preset.ViewPath);

            return enemy;
        }
    }
}