using Core.Infrastructure;
using Core.Random;
using Core.Simulation.Physics2D;
using Core.Simulation.Physics2D.Collisions;
using Core.Timers;
using Features.Damaging.Systems;
using Features.EnemySpawning.Factories;
using Features.EnemySpawning.Systems;
using Features.EnemySpawning.Tags;
using Features.GameOver;
using Features.MovementBehaviours.Forward;
using Features.MovementBehaviours.TowardsPlayer;
using Features.OnDeathCloning;
using Features.PlayerBehaviours.Attack;
using Features.PlayerBehaviours.Movement;
using Features.PlayerBehaviours.Spawn;
using Features.Presets;
using Features.Teleportation;

namespace Features.SimulationImpl
{
    public abstract class AsteroidsSimulation : Core.Infrastructure.Simulation
    {
        private readonly IRandom random;
        private readonly ApplicationState state;
        private readonly ISystemKernel systemKernel;
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;
        private readonly IEntityPresets presets;

        protected AsteroidsSimulation(ApplicationModel appModel, IEntityPresets presets) : base(appModel)
        {
            this.presets = presets;
            this.random = new SystemRandom();
            this.systemKernel = appModel.SystemKernel;
            this.world = appModel.World;
            this.viewKernel = appModel.ViewKernel;
            this.state = appModel.State;
        }

        protected override void InstallSystems()
        {
            InstallCoreSystems();
            InstallSimulation();
            InstallPhysics();
            InstallFightSystems();
            InstallEnemySystems();
            InstallPlayerSystems();
            InstallMovementSystems();
            InstallGameOverSystem();
            InstallKillSystem();
        }

        protected abstract void InstallSimulation();

        private void InstallPhysics()
        {
            this.systemKernel.AddSystem(new Rigidbody2DTranslationSystem(this.world));
            this.systemKernel.AddSystem(new Rigidbody2DRotationSystem(this.world));
            this.systemKernel.AddSystem(new CircleCollisions2DSystem(this.world));
        }

        private void InstallCoreSystems()
        {
            this.systemKernel.AddSystem(new EnvironmentInitialization(this.world));
            this.systemKernel.AddSystem(new TimersSystem(this.world));
        }

        private void InstallMovementSystems()
        {
            this.systemKernel.AddSystem(new TeleportationSystem(this.world));
            this.systemKernel.AddSystem(new MoveTowardsPlayerSystem(this.world));
            this.systemKernel.AddSystem(new MoveForwardSystem(this.world));
        }

        private void InstallPlayerSystems()
        {
            this.systemKernel.AddSystem(new PlayerSpawningSystem(this.world, this.presets.PlayerPreset, this.presets.AttackPresets,
                this.viewKernel));
            this.systemKernel.AddSystem(new PlayerMovementSystem(this.world));
            this.systemKernel.AddSystem(new PlayerAttackSystem(this.world));
        }

        private void InstallEnemySystems()
        {
            var ufoPreset = this.presets.EnemyPresets.Ufo;
            var asteroidPreset = this.presets.EnemyPresets.Asteroid;

            var asteroidsFactory = new AsteroidFactory(this.world, asteroidPreset, this.random, this.viewKernel);
            var ufoFactory = new UfoFactory(this.world, ufoPreset, this.random, this.viewKernel);

            this.systemKernel.AddSystem(new EnemySpawnSystem(asteroidsFactory, this.world, interval: 4));
            this.systemKernel.AddSystem(new EnemySpawnSystem(ufoFactory, this.world, interval: 8));
            this.systemKernel.AddSystem(new EnemyCloningOnDeathSystem(this.world, asteroidsFactory, typeof(Asteroid), this.random));
        }

        private void InstallFightSystems()
        {
            this.systemKernel.AddSystem(new AttackSystem(this.world, this.viewKernel, this.presets.MissilePresets));
            this.systemKernel.AddSystem(new ReloadingSystem(this.world));
            this.systemKernel.AddSystem(new CollisionDamageSystem(this.world));
        }

        private void InstallKillSystem()
        {
            this.systemKernel.AddSystem(new KillSystem(this.world, this.viewKernel));
        }

        private void InstallGameOverSystem()
        {
            this.systemKernel.AddSystem(new GameOverSystem(this.world, this.state));
        }
    }
}