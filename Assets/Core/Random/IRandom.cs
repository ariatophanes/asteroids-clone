namespace Core.Random
{
    public interface IRandom
    {
        public float Next(float min, float max);
        public int Next(int min, int max);
    }
}