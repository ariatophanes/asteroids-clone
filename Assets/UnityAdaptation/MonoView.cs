using Core.Infrastructure;
using UnityEngine;
using Transform = Simulation.Physics2D.Transform;

namespace UnityAdaptation
{
    public class MonoView : MonoBehaviour, IView
    {
        public virtual void OnUpdate(in int id, IWorld world)
        {
            ref var t = ref world.GetComponent<Transform>(id);
            transform.position = new Vector3(t.Position.X, t.Position.Y, 0);
            transform.rotation = Quaternion.Euler(0,0,t.Rotation);
        }

        public void DestroySelf() => GameObject.Destroy(gameObject);
    }
}