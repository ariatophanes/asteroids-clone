using System;

namespace Simulation.Physics2D.Extensions
{
    public static class NumericExtensions
    {
        public static float ToRadians(this float val) => (float) (Math.PI / 180) * val;
    }
}