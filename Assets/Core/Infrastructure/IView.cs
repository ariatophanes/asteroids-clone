namespace Core.Infrastructure
{
    public interface IView
    {
        public void OnUpdate(in int id, IWorld world);
        public void DestroySelf();
    }
}

