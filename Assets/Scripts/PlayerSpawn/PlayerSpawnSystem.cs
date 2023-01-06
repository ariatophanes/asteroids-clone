using System.Linq;
using Core;
using Tags;
using UnityEngine;
using Transform = Simulation.Transform;

namespace PlayerSpawn
{
    public class PlayerSpawnSystem : IGameSystem
    {
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;

        public PlayerSpawnSystem(IWorld world, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
        }

        public void OnStart()
        {
            var player = this.world.NewEntity();

            ref var transform = ref this.world.GetComponent<Transform>(player);
            ref var tag = ref this.world.GetComponent<Player>(player);

            var cam = Camera.main;
            var pos = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            (transform.Position.X, transform.Position.Y) = (pos.x, pos.y);
            (transform.Rotation.X, transform.Rotation.Y, transform.Rotation.Z) = (0, 0, 35);

            this.viewKernel.BindView(player, "Views/Player");
        }

        public void OnUpdate()
        {
            var player = this.world.Filter(typeof(Player)).First();
            ref var transform = ref this.world.GetComponent<Transform>(player);
            transform.Position.Y += 0.0015f;

        }

        public void OnStop() { }
    }
}