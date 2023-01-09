using Core;
using Simulation;
using Simulation.Physics2D;
using UnityAdaptation.InputListener;

namespace UnityAdaptation.Simulation
{
    public class UnitySimulationInstaller
    {
        private readonly ISystemKernel kernel;
        private readonly IWorld world;
        private readonly Controls controls;

        public UnitySimulationInstaller(IWorld world, ISystemKernel kernel, Controls controls)
        {
            this.controls = controls;
            this.world = world;
            this.kernel = kernel;
        }

        public void Setup()
        {
            this.kernel.AddSystem(new EnvironmentInitialization(this.world));
            this.kernel.AddSystem(new InputListeningSystem(this.world, this.controls));
            this.kernel.AddSystem(new TimeCounter(this.world));
            this.kernel.AddSystem(new Rigidbody2DTranslationSystem(this.world));
            this.kernel.AddSystem(new Rigidbody2DRotationSystem(this.world));
            this.kernel.AddSystem(new GamingFieldInitializationSystem(this.world));
        }
    }
}