using Core.Ecs;
using Core.Infrastructure;
using Core.SharedTags;
using Features.Damaging.Components;

namespace Features.Damaging.Systems
{
    public class KillSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;

        public KillSystem(IWorld world, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
        }

        public void OnUpdate()
        {
            var entities = this.world.Filter(typeof(Damageable), typeof(Mortal));

            foreach (var entity in entities)
            {
                ref var damageable = ref this.world.GetComponent<Damageable>(entity);
                if (damageable.ReceivedDamage >= damageable.TolerableDamage) Destroy(entity);
            }
        }

        private void Destroy(in int id)
        {
            this.viewKernel.DestroyView(id);
            this.viewKernel.UnbindView(id);
            this.world.DestroyEntity(id);
        }
    }
}