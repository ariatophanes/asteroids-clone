using Core.Infrastructure;

namespace Core.Ecs
{
    public interface IEntityView
    {
        public void OnUpdate(in int id, IWorld world);
        public void DestroySelf();
    }
}