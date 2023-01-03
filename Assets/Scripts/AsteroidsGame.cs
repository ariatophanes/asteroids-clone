using Core;
using EnemySpawn;
using PlayerSpawn;
using Rules;
using Simulation;
using UnityEngine;

public class AsteroidsGame : Application
{
    public AsteroidsGame(IModelsStorage modelsStorage, IViewKernel viewKernel, ISystemKernel systemKernel) : base(modelsStorage, viewKernel, systemKernel) { }

    protected override void InstallSystems(ISystemKernel kernel, IModelsStorage modelsStorage, IViewKernel viewKernel)
    {
        var config = Resources.Load<GameConfiguration>("Configs/GameConfig");
        
        kernel.AddSystem(new SimulationInitializationSystem(modelsStorage));
        kernel.AddSystem(new RulesInitializationSystem(modelsStorage, config));
        kernel.AddSystem(new PlayerSpawnSystem(modelsStorage, viewKernel));
        kernel.AddSystem(new EnemySpawnSystem(modelsStorage));
    }
}