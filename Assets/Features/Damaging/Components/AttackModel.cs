using System;

namespace Features.Damaging.Components
{
    [Serializable]
    public struct AttackModel
    {
        public float CurrentReloadingTime, ReloadingTime;
        public int AmmoMagazineFull, AmmoMagazine;
        public bool IsEmitting;
    }
}