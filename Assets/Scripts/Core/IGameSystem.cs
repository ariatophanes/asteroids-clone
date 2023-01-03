namespace Core
{
    //TODO: split to IInitializableGameSystem, IUpdatableGameSystem interfaces
    public interface IGameSystem
    {
        public void OnStart();
        public void OnUpdate();
        public void OnStop();
    }
}