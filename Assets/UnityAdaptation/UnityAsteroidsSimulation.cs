using Core.Infrastructure;
using Features.Presets;
using Features.SimulationImpl;
using UnityAdaptation.InputListener;
using UnityAdaptation.Simulation;

namespace UnityAdaptation
{
    public class UnityAsteroidsSimulation : AsteroidsSimulation
    {
        private readonly IWorld world;
        private readonly ISystemKernel systemKernel;

        public UnityAsteroidsSimulation(ApplicationModel appModel, IEntityPresets presets) : base(appModel, presets)
        {
            this.systemKernel = appModel.SystemKernel;
            this.world = appModel.World;
        }
    
        protected override void InstallSimulation()
        {
            //todo: add interface for controls
            var controls = new Controls();
            this.systemKernel.AddSystem(new UnityInputListeningSystem(this.world, controls));
            this.systemKernel.AddSystem(new UnityTimeCounter(this.world));
            this.systemKernel.AddSystem(new UnityGamingFieldInitializationSystem(this.world));
        }
    }
}