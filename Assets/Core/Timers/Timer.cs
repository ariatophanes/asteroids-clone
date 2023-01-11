namespace Core.Timers
{
    public struct Timer
    {
        public float ElapsedTime;
        public float LifeTime;
        public bool IsActive;

        public void Activate(float lifeTime)
        {
            this.LifeTime = lifeTime;
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