namespace Core.Infrastructure
{
    public class ApplicationModel
    {
        public readonly IWorld World;
        public readonly ISystemKernel SystemKernel;
        public readonly IViewKernel ViewKernel;
        public readonly ApplicationState State;

        public ApplicationModel(IWorld world, ISystemKernel systemKernel, IViewKernel viewKernel, ApplicationState state)
        {
            this.World = world;
            this.SystemKernel = systemKernel;
            this.ViewKernel = viewKernel;
            this.State = state;
        }
    }
}