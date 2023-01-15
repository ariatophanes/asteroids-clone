using System;
using System.Linq;
using Core.Collections;

namespace Features.Damaging.Components
{
    public struct Fighter
    {
        private readonly SparseSet indices;
        private readonly AttackModel[] attackModels;

        public Fighter(int attacksCount)
        {
            this.indices = new SparseSet(attacksCount);
            this.attackModels = new AttackModel[attacksCount];
        }
        
        public ref AttackModel GetAttack(in int type) => ref this.attackModels[this.indices.IndexOf(type)];

        public void AddAttack(AttackModel model, int type)
        {
            this.indices.Add(type);
            this.attackModels[this.indices.IndexOf(type)] = model;
        }

        public int GetEmittingAttackType() => Array.FindIndex(this.attackModels, m => m.IsEmitting);

        public int[] GetAllAttackTypes() => this.indices.ToArray();
    }
}