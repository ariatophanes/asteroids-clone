namespace Core
{
    public interface IGameSystem
    {
        public void OnStart();
        public void OnUpdate();
        public void OnStop();
    }
}