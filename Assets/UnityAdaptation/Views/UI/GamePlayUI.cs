using System;
using Core.Infrastructure;
using Features.Damaging.Components;
using TMPro;
using UnityEngine;
using Rigidbody2D = Core.Simulation.Physics2D.Rigidbody2D;
using Transform = Core.Simulation.Common.Transform;

namespace UnityAdaptation.Views.UI
{
    public class GamePlayUI : MonoBehaviour, IEntityView
    {
        private const int FormatDecimalPlaces = 2;

        [SerializeField] private TextMeshProUGUI
            cords,
            rot,
            speed,
            ammos,
            reloading;


        public void OnUpdate(in int id, IWorld world)
        {
            ref var transform = ref world.GetComponent<Transform>(id);
            ref var rb = ref world.GetComponent<Rigidbody2D>(id);
            ref var fighter = ref world.GetComponent<Fighter>(id);

            ref var gun = ref fighter.GetAttack(AttackType.Gun);
            ref var laser = ref fighter.GetAttack(AttackType.Laser);

            var x = (float) Math.Round(transform.Position.X, FormatDecimalPlaces);
            var y = (float) Math.Round(transform.Position.Y, FormatDecimalPlaces);
            var rot = (float) Math.Round(transform.Rotation);
            var speed = (float) Math.Round(rb.LinearMomentumSpeed.Length(), FormatDecimalPlaces);
            var time = (float) Math.Round(laser.ReloadingTime - laser.CurrentReloadingTime, 1);

            this.cords.SetText(SourceTexts.Cords, x, y);
            this.rot.SetText(SourceTexts.Rot, rot, FormatDecimalPlaces);
            this.speed.SetText(SourceTexts.Speed, speed);
            this.ammos.SetText(SourceTexts.Ammos, laser.AmmoMagazine, gun.AmmoMagazine);
            this.reloading.SetText(SourceTexts.Reloading, time);
        }

        public void DestroySelf() => Destroy(gameObject);

        private static class SourceTexts
        {
            public const string Cords = "X:         <color=green>{0}       </color> Y:<color=green>{1}</color>";
            public const string Rot = "Rot:      <color=yellow>{0}</color>";
            public const string Speed = "Speed: <color=yellow>{0}</color>";
            public const string Ammos = "Ammos \nLaser:   <color=red>{0}</color>        Gun: <color=red>{1}</color>";
            public const string Reloading = "Reloading:\nLaser:   <color=white>{0}</color>";
        }
    }
}