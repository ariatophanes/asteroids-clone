using System.Linq;
using Core;
using Presets;
using Tags;
using UnityEngine;
using CircleCollider2D = Simulation.Physics2D.CircleCollider2D;
using Rigidbody2D = Simulation.Physics2D.Rigidbody2D;
using Screen = UnityEngine.Screen;
using Transform = Simulation.Transform;

namespace PlayerSpawn
{
    public class PlayerSpawningSystem : IStartCallbackReceiver
    {
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;

        public PlayerSpawningSystem(IWorld world, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
        }

        public void OnStart()
        {
            var player = this.world.NewEntity();
            var presets = this.world.Filter(typeof(PlayerPreset)).First();

            ref var preset = ref this.world.GetComponent<PlayerPreset>(presets);
            ref var transform = ref this.world.GetComponent<Transform>(player);
            this.world.SetComponent(player, preset.Radius);
            this.world.SetComponent(player, preset.Rb);
            this.world.SetComponent<Player>(player);
            this.world.SetComponent<CircleCollider2D>(player);

            var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            var pos = Camera.main.ScreenToWorldPoint(screenCenter);

            transform.Position.X = pos.x;
            transform.Position.Y = pos.y;

            this.viewKernel.BindView(player, preset.ViewPath);
        }
    }
}