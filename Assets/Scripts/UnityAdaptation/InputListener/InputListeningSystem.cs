using System.Linq;
using Core;
using InputListener;
using PlayerBehaviour;
using UnityEngine;
using Weapons;

namespace UnityAdaptation.InputListener
{
    public class InputListeningSystem  : IStartCallbackReceiver, IUpdateCallbackReceiver, IStopCallbackReceiver
    {
        private readonly IWorld world;
        private readonly Controls controls;

        //todo: pass controls through interface
        public InputListeningSystem(IWorld world, Controls controls)
        {
            this.controls = controls;
            this.world = world;
        }

        public void OnStart()
        {
            this.world.GetComponent<InputFrame>(this.world.NewEntity());
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