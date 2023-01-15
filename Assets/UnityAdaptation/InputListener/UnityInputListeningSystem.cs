using System.Linq;
using Core.Infrastructure;
using Features.InputListening;
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

            input.FirstAttack = this.controls.General.FirstAttack.WasPressedThisFrame();
            input.SecondAttack = this.controls.General.SecondAttack.WasPressedThisFrame();
            
        }

        public void OnStop() => this.controls.Disable();
    }
}