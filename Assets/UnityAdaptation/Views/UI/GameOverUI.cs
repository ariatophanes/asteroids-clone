using Core.Infrastructure;
using Core.Timers;
using TMPro;
using UnityEngine;

namespace UnityAdaptation.Views.UI
{
    public class GameOverUI : MonoBehaviour, IEntityView
    {
        [SerializeField] private TextMeshProUGUI timer;

        public void OnUpdate(in int id, IWorld world)
        {
            ref var timer = ref world.GetComponent<Timer>(id);
            var time = (int) (timer.Interval - timer.ElapsedTime);
            this.timer.SetText(SourceTexts.Message,  time);
        }

        public void DestroySelf() => Destroy(gameObject);

        private static class SourceTexts
        {
            public const string Message = "YOU'RE DEAD\n\nRESTART IN <color=red>{0}</color>";
        }
    }
}