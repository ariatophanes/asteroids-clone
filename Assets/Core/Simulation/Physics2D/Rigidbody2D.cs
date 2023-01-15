using System;
using System.Numerics;

namespace Core.Simulation.Physics2D
{
    [Serializable]
    public struct Rigidbody2D
    {
        public Vector2 LinearForce, LinearMomentumSpeed;
        public float Mass, AngularForce, AngularMomentumSpeed, Deceleration;
        public bool IsKinematic;
    }
}