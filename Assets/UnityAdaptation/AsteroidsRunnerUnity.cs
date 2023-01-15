using Core.Ecs;
using Core.Infrastructure;
using Features.AppStates;
using UnityEngine;

namespace UnityAdaptation
{
    public class AsteroidsRunnerUnity : MonoBehaviour
    {
        private IViewKernel viewKernel;
        private Core.Infrastructure.Simulation simulation;
        private ApplicationFsm fsm;

        private void Start()
        {
            var world = new EcsWorld();
            var systemKernel = new SystemKernel();
            var assetProvider = new UnityAssetFactory();
            var state = new ApplicationState();
            var viewKernel = new ViewKernel(world, assetProvider);
            var appModel = new ApplicationModel(world, systemKernel, viewKernel, state);
            var presets = assetProvider.Load<EntityPresets>("Configs/EntityPresets");

            this.viewKernel = viewKernel;
            this.simulation = new UnityAsteroidsSimulation(appModel, presets);
            this.fsm = new ApplicationFsm(state, world, viewKernel);
            this.simulation.Run();
            this.fsm.ExecuteAction(0);
        }

        private void Update() => this.simulation.Update();

        private void FixedUpdate() => this.simulation.FixedUpdate();

        private void LateUpdate() => this.viewKernel.Update();

        private void OnDestroy()
        {
            this.simulation.Stop();
            this.fsm.Dispose();
        }
    }
}