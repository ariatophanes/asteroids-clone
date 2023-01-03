namespace Core
{
    public interface ISystemKernel
    {
        void Run();
        void Stop();
        void AddSystem(IGameSystem system);
        void Update();
    }
}