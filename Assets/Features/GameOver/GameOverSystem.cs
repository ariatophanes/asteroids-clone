using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.SharedTags;
using Core.Timers;
using Features.Damaging.Components;

namespace Features.GameOver
{
    public class GameOverSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly ApplicationState appState;
        private const int Delay = 5;
        private int gameOverTimer;

        public GameOverSystem(IWorld world, ApplicationState appState)
        {
            this.world = world;
            this.appState = appState;
        }

        public void OnUpdate()
        {
            PreGameOverChecking();
            GameOverChecking();
        }

        private void GameOverChecking()
        {
            if (GameContinues()) return;
            ref var timer = ref this.world.GetComponent<Timer>(this.gameOverTimer);
            if (timer.IsActive) return;
            this.appState.Next();
        }

        private bool GameContinues() => this.gameOverTimer == 0;

        private void PreGameOverChecking()
        {
            if (!GameContinues()) return;

            var player = this.world.Filter(typeof(Player)).First();
            ref var damageable = ref this.world.GetComponent<Damageable>(player);
            
            if (damageable.ReceivedDamage < damageable.TolerableDamage) return;
            
            PreGameOver();
        }

        private void PreGameOver()
        {
            this.gameOverTimer = this.world.NewEntity();
            ref var timer = ref this.world.GetComponent<Timer>(this.gameOverTimer);
            this.world.SetComponent<GameOverTimer>(this.gameOverTimer);
            
            timer.Activate(Delay);
            this.appState.Next();
        }
    }
}