using Core;
using Presets;
using UnityAdaptation;

namespace GameConfigLoading
{
    public class PresetsLoadingSystem : IStartCallbackReceiver
    {
        private readonly IWorld world;
        private readonly EntityPresets presets;

        public PresetsLoadingSystem(IWorld world, EntityPresets presets)
        {
            this.world = world;
            this.presets = presets;
        }

        public void OnStart()
        {
            var presetsEntity = this.world.NewEntity();
            
            this.world.SetComponent(presetsEntity, this.presets.Player);
            this.world.SetComponent(presetsEntity, this.presets.Enemies);
            this.world.SetComponent(presetsEntity, this.presets.Bullets);
        }
    }
}