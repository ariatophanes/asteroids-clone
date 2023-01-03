using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Object = UnityEngine.Object;

public class UnityViewKernel : IViewKernel
{
    private readonly Dictionary<IView, GameObject> gameObjects;
    private readonly Dictionary<string, IView> views;
    private readonly HashSet<string> modelNames;
    private readonly HashSet<string> unresolvedBindings;
    private readonly IModelsStorage modelsStorage;

    public UnityViewKernel(IModelsStorage modelsStorage)
    {
        this.modelsStorage = modelsStorage;
        this.views = new Dictionary<string, IView>(100);
        this.gameObjects = new Dictionary<IView, GameObject>(100);
        this.modelNames = new HashSet<string>(100);
        this.unresolvedBindings = new HashSet<string>(30);
    }

    public void InstantiateModelView(string modelName, string resPath) //todo: add "creator" for easy pooling
    {
        if (this.modelNames.Contains(modelName)) throw new ArgumentException();

        var prefab = Resources.Load<GameObject>(resPath);
        var go = GameObject.Instantiate(prefab);
        var modelView = go.GetComponent<IView>();

        this.views[modelName] = modelView;
        this.gameObjects[modelView] = go;
        this.modelNames.Add(modelName);
    }

    public void DestroyModelView(string modelName)
    {
        if (!this.modelNames.Contains(modelName)) throw new ArgumentException();

        var modelView = this.views[modelName];
        var go = this.gameObjects[modelView];

        this.gameObjects.Remove(modelView);
        this.views.Remove(modelName);
        this.modelNames.Remove(modelName);

        Object.Destroy(go);
    }

    public void UpdateViews()
    {
        foreach (var modelName in this.modelNames.Where(CanResolveBinding))
        {
            this.views[modelName].OnUpdate(modelName, this.modelsStorage);
        }
    }

    private bool CanResolveBinding(string modelName)
    {
        if (this.modelsStorage.HasModel(modelName)) return true;
        if (this.unresolvedBindings.Contains(modelName)) return false;
        this.unresolvedBindings.Add(modelName);
        return false;
    }

    public void FixUnresolvedBindings()
    {
        foreach (var modelName in this.unresolvedBindings) DestroyModelView(modelName);

        this.unresolvedBindings.Clear();
    }
}