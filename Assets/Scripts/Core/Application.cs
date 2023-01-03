using Core;

public abstract class Application
{
    private readonly IModelsStorage modelsStorage;
    private readonly IViewKernel viewKernel;
    private readonly ISystemKernel systemKernel;

    protected Application(IModelsStorage modelsStorage, IViewKernel viewKernel, ISystemKernel systemKernel)
    {
        this.viewKernel = viewKernel;
        this.systemKernel = systemKernel;
        this.modelsStorage = modelsStorage;
    }

    protected abstract void InstallSystems(ISystemKernel kernel, IModelsStorage models, IViewKernel viewKernel);

    public void Run()
    {
        InstallSystems(this.systemKernel, this.modelsStorage, this.viewKernel);
        this.systemKernel.Run();
    }

    public void Update()
    {
        this.systemKernel.Update();
        this.viewKernel.UpdateViews();
        this.viewKernel.FixUnresolvedBindings();
    }

    public void Stop()
    {
        this.systemKernel.Stop();
        this.modelsStorage.Dispose();
    }
}

public interface IViewKernel
{
    void InstantiateModelView(string modelName, string resPath);
    void DestroyModelView(string modelName);
    void UpdateViews();
    void FixUnresolvedBindings();
}