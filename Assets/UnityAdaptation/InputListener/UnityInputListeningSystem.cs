using System.Linq;
using Core;
using Core.Ecs;
using Core.Infrastructure;
using InputListening;
using PlayerBehaviour;
using PlayerBehaviour.Attack;
using UnityEngine;

namespace UnityAdaptation.InputListener
{
    public class UnityInputListeningSystem  : IStartCallbackReceiver, IUpdateCallbackReceiver, IStopCallbackReceiver
    {
        private readonly IWorld world;
        private readonly Controls controls;

        //todo: pass controls through interface
        public UnityInputListeningSystem(IWorld world, Controls controls)
        {
            this.controls = controls;
            this.world = world;
        }

        public void OnStart()
        {
            this.world.SetComponent<InputFrame>(this.world.NewEntity());
            this.controls.Enable();
        }

        public void OnUpdate()
        {
            var inputEnt = this.world.Filter(typeof(InputFrame)).First();
            ref var input = ref this.world.GetComponent<InputFrame>(inputEnt);

            var moveAxis = this.controls.General.Move.ReadValue<Vector2>();
            input.HorizontalAxis = moveAxis.x;
            input.VerticalAxis = moveAxis.y;

            var firstAttack = this.controls.General.FirstAttack.WasPressedThisFrame();
            var secondAttack = this.controls.General.SecondAttack.WasPressedThisFrame();

            input.AttackType = (firstAttack, secondAttack) switch
            {
                (true, true) => AttackType.Gun,
                (true, false) => AttackType.Gun,
                (false, true) => AttackType.Laser,
                (false, false) => AttackType.None
            };
        }

        public void OnStop() => this.controls.Disable();
    }
}