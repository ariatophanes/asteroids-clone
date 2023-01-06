using UnityEngine;
using Transform = Simulation.Transform;

namespace Core
{
    public class MonoView : MonoBehaviour, IView
    {
        public virtual void OnUpdate(int id, IWorld world)
        {
            ref var t = ref world.GetComponent<Transform>(id);
            transform.position = new Vector3(t.Position.X, t.Position.Y, t.Position.Z);
            transform.eulerAngles = new Vector3(t.Rotation.X, t.Rotation.Y, t.Rotation.Z);            
        }

    }
}