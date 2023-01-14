using Core.Infrastructure;
using UnityAdaptation.InputListener;
using UnityAdaptation.Simulation;

namespace UnityAdaptation
{
    public class UnityAsteroids : Asteroids
    {
        private readonly IWorld world;
        private readonly ISystemKernel systemKernel;

        public UnityAsteroids(IWorld world, ISystemKernel systemKernel, IViewKernel viewKernel, IActorFactory actorFactory) : base(world, systemKernel, viewKernel, actorFactory)
        {
            this.systemKernel = systemKernel;
            this.world = world;
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