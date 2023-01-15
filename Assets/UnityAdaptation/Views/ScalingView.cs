using Core.Infrastructure;
using Core.Simulation.Common;
using UnityEngine;

namespace UnityAdaptation.Views
{
    public sealed class ScalingView : MonoBehaviour, IEntityView
    {
        private const float K = 1.5f;
        private Vector3 s;
        
        private void Awake() => s = Vector3.one;
        
        public void OnUpdate(in int id, IWorld world)
        {
            ref var r = ref world.GetComponent<Radius>(id);
            transform.localScale = r.Value * s * K;
        }

        public void DestroySelf() => Destroy(this);
    }
}