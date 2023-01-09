namespace Core
{
    public interface IViewKernel
    {
        void BindView(int id, string resPath);
        void DestroyView(int id);
        void Update();
        void FixUnresolvedBindings();
    }
}