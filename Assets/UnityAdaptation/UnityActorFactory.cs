using Core.Ecs;
using UnityEngine;

namespace UnityAdaptation
{
    public class UnityActorFactory : IActorFactory
    {
        public IEntityView[] InstantiateActor(string path) => GameObject.Instantiate(Resources.Load<GameObject>(path)).GetComponentsInChildren<IEntityView>();
        public T Load<T>(string path) where T : class => Resources.Load(path) as T;
    }
}