using Core.Infrastructure;
using Core.Random;
using Core.Timers;
using Core.ViewBindingAutomation;
using Damaging.Systems;
using DeathProcessing.AsteroidsDeathProcessing;
using EnemySpawning;
using EnemySpawning.Tags;
using MovementBehaviour.Forward;
using MovementBehaviour.TowardsPlayer;
using PlayerBehaviour.Attack;
using PlayerBehaviour.Collisions;
using PlayerBehaviour.Movement;
using PlayerBehaviour.Spawn;
using Simulation.Physics2D;
using Simulation.Physics2D.Collisions;
using Teleportation;
using UnityAdaptation;
using UnityEngine;
using Application = Core.Infrastructure.Application;

public abstract class Game : Application
{
    private IWorld world;
    private IViewKernel viewKernel;
    private ISystemKernel systemKernel;
    private IRandom random;

    protected Game(IWorld world, IViewKernel viewKernel, ISystemKernel systemKernel) : base(world, viewKernel, systemKernel) { }

    protected override void InstallSystems(ISystemKernel systemKernel, IWorld world, IViewKernel viewKernel)
    {
        this.systemKernel = systemKernel;
        this.viewKernel = viewKernel;
        this.world = world;
        this.random = new SystemRandom();

        var presets = Resources.Load<EntityPresets>("Configs/EntityPresets");

        InstallCoreSystems();
        InstallSimulation();
        InstallPhysics();
        InstallEnemySystems(presets);
        InstallPlayerSystems(presets);
        InstallMovementSystems();
        InstallCollisionSystems();
        ViewBindingsAutomation();
    }

    protected abstract void InstallSimulation();

    private void InstallPhysics()
    {
        this.systemKernel.AddSystem(new Rigidbody2DTranslationSystem(this.world));
        this.systemKernel.AddSystem(new Rigidbody2DRotationSystem(this.world));
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

    private void InstallCollisionSystems()
    {
        this.systemKernel.AddSystem(new CollisionDamageSystem(this.world));
        this.systemKernel.AddSystem(new PlayerCollisionsSystem(this.world));
        this.systemKernel.AddSystem(new KillSystem(this.world));
        this.systemKernel.AddSystem(new CircleCollisions2DSystem(this.world));
    }

    private void InstallPlayerSystems(EntityPresets presets)
    {
        var bulletFactory = new MissileFactory(this.world, presets.MissilePresets.Gun);
        var laserFactory = new MissileFactory(this.world, presets.MissilePresets.Laser);

        this.systemKernel.AddSystem(new PlayerSpawningSystem(this.world, presets));
        this.systemKernel.AddSystem(new PlayerMovementSystem(this.world));
        this.systemKernel.AddSystem(new PlayerAttackSystem(this.world, bulletFactory, AttackType.Gun));
        this.systemKernel.AddSystem(new PlayerAttackSystem(this.world, laserFactory, AttackType.Laser));
    }

    private void InstallEnemySystems(EntityPresets presets)
    {
        var ufoPreset = presets.EnemyPresets.Ufo;
        var asteroidPreset = presets.EnemyPresets.Asteroid;

        var asteroidsFactory = new AsteroidFactory(this.world, asteroidPreset, this.random);
        var ufoFactory = new UfoFactory(this.world, ufoPreset, this.random);

        this.systemKernel.AddSystem(new EnemySpawnSystem(asteroidsFactory, this.world, interval: 5));
        this.systemKernel.AddSystem(new EnemySpawnSystem(ufoFactory, this.world, interval: 15));
        this.systemKernel.AddSystem(new EnemyOnDeathCloningSystem(this.world, asteroidsFactory, typeof(Asteroid), this.random));
    }

    private void ViewBindingsAutomation()
    {
        this.systemKernel.AddSystem(new ViewBindingAutomationSystem(this.world, this.viewKernel));
    }
}