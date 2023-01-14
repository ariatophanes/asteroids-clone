using Core.ViewBindingAutomation;

namespace Core.Infrastructure
{
    public abstract class Application
    {
        private readonly IWorld world;
        private readonly ISystemKernel systemKernel;
        private readonly IViewKernel viewKernel;

        protected Application(IWorld world, ISystemKernel systemKernel)
        {
            this.systemKernel = systemKernel;
            this.world = world;
        }

        protected abstract void InstallSystems();
        
        //todo: extract methods into interface
        public void Run()
        {
            InstallSystems();
            this.systemKernel.Run();
        }

        public void Update()
        {
            this.systemKernel.Update();
        }

        public void Stop()
        {
            this.systemKernel.Stop();
            this.world.Dispose();
        }

        public void FixedUpdate()
        {
            this.systemKernel.FixedUpdate();
        }
    }
}