using System;
using Simulation.Physics2D;

namespace Presets
{
    [Serializable]
    public struct PlayerPreset
    {
        public Rigidbody2D Rb;
        public Radius Radius;
        public string ViewPath;
    }
}