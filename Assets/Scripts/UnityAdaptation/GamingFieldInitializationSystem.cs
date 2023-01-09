using Core;
using Simulation;
using UnityEngine;

namespace UnityAdaptation
{
    public class GamingFieldInitializationSystem : IStartCallbackReceiver
    {
        private readonly IWorld world;

        public GamingFieldInitializationSystem(IWorld world) => this.world = world;

        public void OnStart()
        {
            ref var bounds = ref this.world.GetComponent<GamingFieldBounds>(this.world.NewEntity());
            var cam = Camera.main;
            var trp = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            var lbp = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));

            (bounds.BoundsVertical.X, bounds.BoundsVertical.Y) = (lbp.x, trp.y);
            (bounds.BoundsHorizontal.X, bounds.BoundsHorizontal.Y) = (lbp.x, trp.x);
        }
    }
}