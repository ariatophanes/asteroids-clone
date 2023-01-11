using System;
using Core.Ecs;
using Core.Infrastructure;
using Core.Random;
using EnemySpawning;

namespace DeathProcessing.AsteroidsDeathProcessing
{
    public class EnemyOnDeathCloningSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly Type tag;
        private readonly EnemyFactory factory;
        private readonly IRandom random;

        public EnemyOnDeathCloningSystem(IWorld world, EnemyFactory factory, Type tag, IRandom random)
        {
            this.random = random;
            this.tag = tag;
            this.factory = factory;
            this.world = world;
        }

        public void OnUpdate()
        {
            // var deadEntities = this.world.Filter(typeof(Dead), this.tag);
            //
            // foreach (var entity in deadEntities)
            // {
            //     ref var transform = ref this.world.GetComponent<Transform>(entity);
            //     ref var rigidbody = ref this.world.GetComponent<Rigidbody2D>(entity);
            //     ref var damageable = ref this.world.GetComponent<Damageable>(entity);
            //     ref var radius = ref this.world.GetComponent<Radius>(entity);
            //     
            //     var clonesCount = this.random.Next(2, 4);
            //     
            //     for (var i = 0; i < clonesCount; i++)
            //     {
            //         var clone =  this.factory.Create();
            //         ref var cloneRadius = ref this.world.GetComponent<Radius>(clone);
            //         ref var cloneRigidbody = ref this.world.GetComponent<Rigidbody2D>(clone);
            //         ref var cloneTransform = ref this.world.GetComponent<Transform>(clone);
            //         ref var cloneDamageable = ref this.world.GetComponent<Damageable>(clone);
            //         
            //         cloneTransform = transform;
            //         cloneRigidbody = rigidbody;
            //         cloneDamageable.TolerableDamage = damageable.TolerableDamage / 2;
            //         cloneRigidbody.Mass /= 2;
            //         cloneRadius.Value = radius.Value / 2;
            //     }
            // }
        }
    }
}