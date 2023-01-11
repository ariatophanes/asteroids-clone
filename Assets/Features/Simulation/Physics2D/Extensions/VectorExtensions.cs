using System;
using System.Numerics;

namespace Simulation.Physics2D.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 Rotate(this Vector2 v, double degrees) => new(
            x: (float) (v.X * Math.Cos(degrees) - v.Y * Math.Sin(degrees)),
            y: (float) (v.X * Math.Sin(degrees) + v.Y * Math.Cos(degrees))
        );
    }
}