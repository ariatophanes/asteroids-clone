using System.Numerics;

namespace Simulation
{
    public struct Transform
    {
        public Vector2 Position;
        public float Rotation;

        public Transform(Vector2 position, float rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public Transform(float x, float y, float rotation)
        {
            this.Position = new Vector2(x,y);
            this.Rotation = rotation;
        }
    }
}