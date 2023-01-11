using System;
using System.Numerics;

namespace Simulation.Physics2D
{
    [Serializable]
    public struct Rigidbody2D
    {
        public Vector2 LinearForce, LinearMomentumSpeed;
        public float Mass, AngularForce, AngularMomentumSpeed, Deceleration;
    }
}