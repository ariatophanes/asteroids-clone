using Core.Ecs;
using UnityEngine;
using Application = Core.Infrastructure.Application;

namespace UnityAdaptation
{
    public class Bootstrap : MonoBehaviour
    {
        private Application app;

        private void Awake()
        {
            var world = new EcsWorld();
            var assetProvider = new AssetProviderRes();
            var systemKernel = new UnitySystemKernel();
            var viewKernel = new UnityViewKernel(world, assetProvider);

            this.app = new UnityGame(world, viewKernel, systemKernel);
            this.app.Run();
        }

        private void Update() => this.app.Update();

        private void FixedUpdate() => this.app.FixedUpdate();

        private void LateUpdate() => this.app.LateUpdate();

        private void OnDestroy() => this.app.Stop();
    }
}