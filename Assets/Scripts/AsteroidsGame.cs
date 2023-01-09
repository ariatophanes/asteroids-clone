using Core;
using EnemySpawning;
using GameConfigLoading;
using MovementBehaviour;
using PlayerBehaviour;
using PlayerSpawn;
using PlayerTeleportation;
using UnityAdaptation;
using UnityAdaptation.Simulation;
using UnityEngine;
using Application = Core.Application;

public class AsteroidsGame : Application
{
    public AsteroidsGame(IWorld world, IViewKernel viewKernel, ISystemKernel systemKernel) : base(world, viewKernel, systemKernel) { }

    protected override void InstallSystems(ISystemKernel kernel, IWorld world, IViewKernel viewKernel)
    {
        var presets = Resources.Load<EntityPresets>("Configs/EntityPresets");
        var controls = new Controls();

        new UnitySimulationInstaller(world, kernel, controls).Setup();
        kernel.AddSystem(new TimersSystem(world));
        kernel.AddSystem(new PresetsLoadingSystem(world, presets));
        kernel.AddSystem(new PlayerSpawningSystem(world, viewKernel));
        kernel.AddSystem(new EnemySpawnSystem(world, viewKernel));
        kernel.AddSystem(new PlayerMovementSystem(world));
        kernel.AddSystem(new TeleportationSystem(world));
        kernel.AddSystem(new PlayerShootingSystem(world, viewKernel));
        kernel.AddSystem(new MoveToPlayerSystem(world));
        kernel.AddSystem(new MoveForwardSystem(world));
    }
}