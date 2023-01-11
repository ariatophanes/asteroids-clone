using Core.Ecs;
using Core.Infrastructure;

namespace Core.ViewBindingAutomation
{
    public class ViewBindingAutomationSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;

        public ViewBindingAutomationSystem(IWorld world, IViewKernel viewKernel)
        {
            this.world = world;
            this.viewKernel = viewKernel;
        }

        public void OnUpdate()
        {
            var entities = this.world.Filter(typeof(AutoViewBinding));
            foreach (var entity in entities)
            {
                ref var bindEvent = ref this.world.GetComponent<AutoViewBinding>(entity);
                this.viewKernel.BindView(entity, bindEvent.ViewPath);
                this.world.RemoveComponent<AutoViewBinding>(entity);
            }
        }
    }
}