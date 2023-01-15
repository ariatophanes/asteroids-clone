using System;
using UnityEngine.Serialization;

namespace Core.Timers
{
    [Serializable]
    public struct Timer
    {
        public float ElapsedTime;
        [FormerlySerializedAs("LifeTime")] public float Interval;
        public bool IsActive;

        public void Activate(float lifeTime)
        {
            this.Interval = lifeTime;
            this.IsActive = true;
        }

        public void Restart()
        {
            this.IsActive = true;
            this.ElapsedTime = 0;
        }

        public void Deactivate()
        {
            this.IsActive = false;
        }
    }
}