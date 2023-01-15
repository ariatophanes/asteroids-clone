namespace Core.Infrastructure
{
    public interface IEntityView
    {
        public void OnUpdate(in int id, IWorld world);
        public void DestroySelf();
    }
}