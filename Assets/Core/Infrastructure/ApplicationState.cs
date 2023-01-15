using System;

namespace Core.Infrastructure
{
    public sealed class ApplicationState
    {
        public int CurrentState { get; private set; }
        public event Action<int> Changed;

        public void Set(int newState)
        {
            CurrentState = newState;
            Changed?.Invoke(newState);
        }

        public void Next() => Set(CurrentState + 1);
    }
}