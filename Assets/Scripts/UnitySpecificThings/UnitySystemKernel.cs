using System.Collections.Generic;

namespace Core
{

    public class UnitySystemKernel : ISystemKernel
    {
        private readonly LinkedList<IGameSystem> systems;

        public UnitySystemKernel() => this.systems = new LinkedList<IGameSystem>();

        public void AddSystem(IGameSystem system) => this.systems.AddLast(system);

        public void Run()
        {
            foreach (var system in systems) system.OnStart();
        }

        public void Update()
        {
            foreach (var system in systems) system.OnUpdate();
        }

        public void Stop()
        {
            foreach (var system in systems) system.OnStop();
        }
    }
}