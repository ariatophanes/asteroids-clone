namespace Core
{
    public interface ISystemKernel
    {
        void Run();
        void Stop();
        void AddSystem(object system);
        void Update();
        void FixedUpdate();
    }
}