using UnityEngine;
using Transform = Simulation.Transform;

namespace Core
{
    public class View : MonoBehaviour, IView
    {
        public virtual void OnUpdate(string modelName, IModelsStorage models)
        {
            if (models.HasComponent<Transform>(modelName)) UpdateTransform(modelName, models);
        }

        private void UpdateTransform(string modelName, IModelsStorage models)
        {
            ref var t = ref models.GetComponent<Transform>(modelName);
            transform.position = new Vector3(t.Position.X, t.Position.Y, t.Position.Z);
            transform.eulerAngles = new Vector3(t.Rotation.X, t.Rotation.Y, t.Rotation.Z);
        }

    }
}