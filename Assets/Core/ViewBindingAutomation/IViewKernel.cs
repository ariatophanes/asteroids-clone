namespace Core.ViewBindingAutomation
{
    public interface IViewKernel
    {
        void BindView(in int id, string path);
        void UnbindView(in int id);
        void DestroyView(in int id);
        void Update();
    }
}