using System;
using System.Numerics;
using Core.Ecs;
using Core.Infrastructure;
using Core.Random;
using Damaging.Components;
using EnemySpawning;
using Simulation.Physics2D;

namespace DeathProcessing.AsteroidsDeathProcessing
{
    public class EnemyCloningOnDeathSystem : IUpdateCallbackReceiver
    {
        private readonly IWorld world;
        private readonly Type tag;
        private readonly EnemyFactory factory;
        private readonly IRandom random;

        public EnemyCloningOnDeathSystem(IWorld world, EnemyFactory factory, Type tag, IRandom random)
        {
            this.random = random;
            this.tag = tag;
            this.factory = factory;
            this.world = world;
        }

        public void OnUpdate()
        {
            var deadEntities = this.world.Filter(typeof(Mortal), this.tag);

            foreach (var entity in deadEntities)
            {
                ref var mortal = ref this.world.GetComponent<Mortal>(entity);

                if (!mortal.IsDead) continue;

                ref var rigidbody = ref this.world.GetComponent<Rigidbody2D>(entity);
                ref var transform = ref this.world.GetComponent<Transform>(entity);
                ref var damageable = ref this.world.GetComponent<Damageable>(entity);
                ref var radius = ref this.world.GetComponent<Radius>(entity);

                if (rigidbody.Mass <= 1) continue;

                var clonesCount = new SystemRandom(entity).Next(2, 4);
                
                for (var i = 0; i < clonesCount; i++)
                {
                    var clone = this.factory.Create();
                    ref var cloneRadius = ref this.world.GetComponent<Radius>(clone);
                    ref var cloneRigidbody = ref this.world.GetComponent<Rigidbody2D>(clone);
                    ref var cloneTransform = ref this.world.GetComponent<Transform>(clone);
                    ref var cloneDamageable = ref this.world.GetComponent<Damageable>(clone);
                    
                    cloneTransform = transform;
                    cloneDamageable.TolerableDamage = damageable.TolerableDamage / 2;
                    cloneRigidbody.Mass = rigidbody.Mass / (clonesCount);
                    cloneRadius.Value = radius.Value / (clonesCount - 1);
                    cloneTransform.Rotation = this.random.Next(-180, 180);
                }
            }
        }
    }
}