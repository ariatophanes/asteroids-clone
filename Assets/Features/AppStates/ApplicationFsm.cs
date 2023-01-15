using System;
using System.Linq;
using Core.Infrastructure;
using Core.SharedTags;
using Core.Timers;
using UnityEngine.SceneManagement;

namespace Features.AppStates
{
    public class ApplicationFsm : IDisposable
    {
        private readonly ApplicationState state;
        private readonly IWorld world;
        private readonly IViewKernel viewKernel;
        private readonly Action[] fsm;

        public ApplicationFsm(ApplicationState state, IWorld world, IViewKernel viewKernel)
        {
            this.viewKernel = viewKernel;
            this.world = world;
            this.state = state;

            this.fsm = new Action[] {GamePlay, GameOver, Restart};
            this.state.Changed += ExecuteAction;
        }

        private void GamePlay()
        {
            var player = this.world.Filter(typeof(Player)).First();
            this.viewKernel.BindView(player, "Views/UI/Gameplay UI");
        }

        private void GameOver()
        {
            var gameOverTimer = this.world.Filter(typeof(GameOverTimer)).First();
            this.viewKernel.BindView(gameOverTimer, "Views/UI/Gameover UI");
        }

        private void Restart() => SceneManager.LoadScene(0);
        
        public void ExecuteAction(int action) => this.fsm[action].Invoke();

        public void Dispose() => this.state.Changed -= ExecuteAction;
    }
}