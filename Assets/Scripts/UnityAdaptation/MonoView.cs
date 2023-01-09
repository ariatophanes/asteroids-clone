using Core;
using UnityEngine;
using Transform = Simulation.Transform;

namespace UnityAdaptation
{
    public class MonoView : MonoBehaviour, IView
    {
        public virtual void OnUpdate(int id, IWorld world)
        {
            ref var t = ref world.GetComponent<Transform>(id);
            transform.position = new Vector3(t.Position.X, t.Position.Y, 0);
            transform.rotation = Quaternion.Euler(0,0,t.Rotation);          
        }

    }
}