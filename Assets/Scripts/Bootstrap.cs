using System;
using Core;
using UnityEngine;
using UnitySpecificThings;

public class Bootstrap : MonoBehaviour
{
    private EcsApplication app;

    private void Start()
    {
        var world = new World();
        var assetProvider = new AssetProviderRes();
        var systemKernel = new UnitySystemKernel();
        var viewKernel = new UnityViewKernel(world, assetProvider);
        
        this.app = new AsteroidsGame(world, viewKernel, systemKernel);
        this.app.Run();
    }

    private void Update() => this.app.Update();

    private void OnDestroy() => this.app.Stop();
}