using Core.Ecs;
using Core.Infrastructure;
using Simulation.Physics2D;
using UnityEngine;

namespace UnityAdaptation.Views
{
    public sealed class ScalingView : MonoBehaviour, IEntityView
    {
        private Vector3 s;
        
        private void Awake() => s = Vector3.one;
        
        public void OnUpdate(in int id, IWorld world)
        {
            ref var r = ref world.GetComponent<Radius>(id);
            transform.localScale = r.Value * s;
        }

        public void DestroySelf() => Destroy(this);
    }
}