namespace Core.Infrastructure
{
    public abstract class Simulation
    {
        private readonly IWorld world;
        private readonly ISystemKernel systemKernel;
        private readonly IViewKernel viewKernel;

        protected Simulation(ApplicationModel appModel)
        {
            this.systemKernel = appModel.SystemKernel;
            this.world = appModel.World;
        }

        public void Run()
        {
            InstallSystems();
            this.systemKernel.Run();
        }

        public void Stop()
        {
            this.systemKernel.Stop();
            this.world.Dispose();
        }

        protected abstract void InstallSystems();

        public void Update() => this.systemKernel.Update();

        public void FixedUpdate() => this.systemKernel.FixedUpdate();
    }
}