namespace Core
{
    public abstract class Application
    {
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;
        private readonly ISystemKernel systemKernel;

        protected Application(IWorld world, IViewKernel viewKernel, ISystemKernel systemKernel)
        {
            this.viewKernel = viewKernel;
            this.systemKernel = systemKernel;
            this.world = world;
        }

        protected abstract void InstallSystems(ISystemKernel kernel, IWorld models, IViewKernel viewKernel);

        public void Run()
        {
            InstallSystems(this.systemKernel, this.world, this.viewKernel);
            this.systemKernel.Run();
        }

        public void Update()
        {
            this.systemKernel.Update();
            this.viewKernel.FixUnresolvedBindings();
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

        public void LateUpdate()
        {
            this.viewKernel.Update();
        }
    }
}