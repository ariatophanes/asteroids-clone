using System;
using Core;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private Application app;

    private void Start()
    {
        var modelsStorage = new ModelsStorage();
        
        this.app = new AsteroidsGame(modelsStorage, new UnityViewKernel(modelsStorage), new UnitySystemKernel());
        this.app.Run();
    }

    private void Update()
    {
        this.app.Update();
    }

    private void OnDestroy()
    {
        this.app.Stop();
    }
}