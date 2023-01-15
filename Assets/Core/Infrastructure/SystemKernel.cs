using System.Collections.Generic;

namespace Core.Infrastructure
{
    public class SystemKernel : ISystemKernel
    {
        private readonly LinkedList<IStartCallbackReceiver> start;
        private readonly LinkedList<IUpdateCallbackReceiver> update;
        private readonly LinkedList<IFixedUpdateCallbackReceiver> fixedUpdate;
        private readonly LinkedList<IStopCallbackReceiver> stop;

        public SystemKernel()
        {
            this.start = new LinkedList<IStartCallbackReceiver>();
            this.update = new LinkedList<IUpdateCallbackReceiver>();
            this.fixedUpdate = new LinkedList<IFixedUpdateCallbackReceiver>();
            this.stop = new LinkedList<IStopCallbackReceiver>();
        }

        public void AddSystem(object system)
        {
            if (system is IStartCallbackReceiver startCallbackReceiver) this.start.AddLast(startCallbackReceiver);
            if (system is IUpdateCallbackReceiver updateCallbackReceiver) this.update.AddLast(updateCallbackReceiver);
            if (system is IFixedUpdateCallbackReceiver fixedUpdateCallbackReceiver) this.fixedUpdate.AddLast(fixedUpdateCallbackReceiver);
            if (system is IStopCallbackReceiver stopCallbackReceiver) this.stop.AddLast(stopCallbackReceiver);
        }

        public void Run()
        {
            foreach (var system in this.start) system.OnStart();
        }

        public void Update()
        {
            foreach (var system in this.update) system.OnUpdate();
        }

        public void FixedUpdate()
        {
            foreach (var system in this.fixedUpdate) system.OnFixedUpdate();
        }
        
        public void Stop()
        {
            foreach (var system in this.stop) system.OnStop();
        }
    }
}