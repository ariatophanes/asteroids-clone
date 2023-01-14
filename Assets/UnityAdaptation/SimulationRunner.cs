using System;
using Core.Ecs;
using Core.Infrastructure;
using UnityEngine;
using Application = Core.Infrastructure.Application;

namespace UnityAdaptation
{
    public class SimulationRunner : MonoBehaviour
    {
        private Application app;
        private ViewKernel viewKernel;

        private void Start()
        {
            var world = new EcsWorld();
            var systemKernel = new UnitySystemKernel();
            var assetProvider = new UnityActorFactory();
            this.viewKernel = new ViewKernel(world, assetProvider);

            this.app = new UnityAsteroids(world, systemKernel, this.viewKernel, assetProvider);
            this.app.Run();
        }

        private void Update() => this.app.Update();

        private void FixedUpdate() => this.app.FixedUpdate();

        private void LateUpdate() => this.viewKernel.Update();

        private void OnDestroy() => this.app.Stop();
    }
}