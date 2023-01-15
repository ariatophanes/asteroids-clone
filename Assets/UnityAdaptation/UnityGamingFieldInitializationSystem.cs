using Core.Infrastructure;
using Core.Simulation.Common;
using UnityEngine;

namespace UnityAdaptation
{
    public class UnityGamingFieldInitializationSystem : IStartCallbackReceiver
    {
        private readonly IWorld world;

        public UnityGamingFieldInitializationSystem(IWorld world) => this.world = world;

        public void OnStart()
        {
            ref var bounds = ref this.world.GetComponent<GamingField>(this.world.NewEntity());
            var cam = Camera.main;
            var trp = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            var lbp = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));

            (bounds.BoundsVertical.X, bounds.BoundsVertical.Y) = (lbp.x, trp.y);
            (bounds.BoundsHorizontal.X, bounds.BoundsHorizontal.Y) = (lbp.x, trp.x);
        }
    }
}