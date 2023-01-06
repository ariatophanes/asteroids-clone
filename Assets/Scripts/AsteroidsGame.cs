using Core;
using EnemySpawn;
using PlayerSpawn;
using Rules;
using Simulation;
using UnityEngine;

public class AsteroidsGame : EcsApplication
{
    public AsteroidsGame(IWorld world, IViewKernel viewKernel, ISystemKernel systemKernel) : base(world, viewKernel, systemKernel) { }

    protected override void InstallSystems(ISystemKernel kernel, IWorld world, IViewKernel viewKernel)
    {
        var config = Resources.Load<GameConfiguration>("Configs/GameConfig");
        
        kernel.AddSystem(new SimulationInitializationSystem(world));
        kernel.AddSystem(new ModelsInitializationSystem(world, config));
        kernel.AddSystem(new PlayerSpawnSystem(world, viewKernel));
        kernel.AddSystem(new EnemySpawnSystem(world));
    }
}