namespace Core.Random
{
    public class SystemRandom : IRandom
    {
        private readonly System.Random random;

        public SystemRandom() => this.random = new System.Random();
        public SystemRandom(int seed) => this.random = new System.Random(seed);

        public float Next(float min, float max) => (float) (this.random.NextDouble() * (max - min) + min);

        public int Next(int min, int max) => this.random.Next(min, max);
    }
}