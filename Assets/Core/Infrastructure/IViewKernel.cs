namespace Core.Infrastructure
{
    public interface IViewKernel
    {
        void BindView(int id, string resPath);
        void Update();
    }
}