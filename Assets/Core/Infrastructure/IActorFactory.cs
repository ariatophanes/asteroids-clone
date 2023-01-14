using Core.Ecs;

namespace UnityAdaptation
{
    public interface IActorFactory
    {
        IEntityView[] InstantiateActor(string path);
        T Load<T>(string path) where T : class; //todo: move to "asset provider" class
    }
}