namespace Core.Infrastructure
{
    public interface IView
    {
        public void OnUpdate(int id, IWorld world);
        public void DestroySelf();
    }
}

