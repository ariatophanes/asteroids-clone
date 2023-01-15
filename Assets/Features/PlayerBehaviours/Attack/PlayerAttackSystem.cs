using System.Linq;
using Core.Ecs;
using Core.Infrastructure;
using Core.SharedTags;
using Features.Damaging.Components;
using Features.InputListening;

namespace Features.PlayerBehaviours.Attack
{
    public class PlayerAttackSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;

        public PlayerAttackSystem(IWorld world) => this.world = world;

        public void OnUpdate()
        {
            var playerEntities = this.world.Filter(typeof(Player), typeof(Fighter));
            var inputEnt = this.world.Filter(typeof(InputFrame)).First();

            foreach (var player in playerEntities)
            {
                ref var input = ref this.world.GetComponent<InputFrame>(inputEnt);
                ref var fighter = ref this.world.GetComponent<Fighter>(player);

                ref var firstAttack = ref fighter.GetAttack(AttackType.Gun);
                ref var secondAttack = ref fighter.GetAttack(AttackType.Laser);

                (firstAttack.IsEmitting, secondAttack.IsEmitting) = (input.FirstAttack, input.SecondAttack);
            }
        }
    }
}