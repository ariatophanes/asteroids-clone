using System;
using Core.Simulation.Common;
using Core.Simulation.Physics2D;

namespace Features.PlayerBehaviours.Presets
{
    [Serializable]
    public struct PlayerPreset
    {
        public Rigidbody2D Rb;
        public Radius Radius;
        public string ViewPath;
    }
}