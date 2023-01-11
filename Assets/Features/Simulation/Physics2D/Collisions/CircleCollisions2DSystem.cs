using System.Collections.Generic;
using Core.Ecs;
using Core.Infrastructure;

namespace Simulation.Physics2D.Collisions
{
    public class CircleCollisions2DSystem : IFixedUpdateCallbackReceiver
    {
        private const int MaxCollisionsPerEntity = 5;
        private readonly IWorld world;
        private readonly Stack<int> entitiesToClean;

        public CircleCollisions2DSystem(IWorld world)
        {
            this.entitiesToClean = new Stack<int>(10);
            this.world = world;
        }

        public void OnFixedUpdate()
        {
            var entities = this.world.Filter(typeof(CircleCollider2D), typeof(Radius), typeof(Transform));

            //todo: figure out how to use span here
            while (this.entitiesToClean.Count > 0)
            {
                var entity = this.entitiesToClean.Pop();
                ref var collidedEntities = ref this.world.GetComponent<CollidedEntities>(entity);
                collidedEntities.Entities.Clear();
            }

            foreach (var self in entities)
            {
                ref var selfRadius = ref this.world.GetComponent<Radius>(self);
                ref var selfTransform = ref this.world.GetComponent<Transform>(self);
                ref var selfCollidedEntities = ref this.world.GetComponent<CollidedEntities>(self);
                selfCollidedEntities.Entities ??= new Stack<int>(MaxCollisionsPerEntity);

                foreach (var other in entities)
                {
                    if (self == other) continue;

                    ref var otherRadius = ref this.world.GetComponent<Radius>(other);
                    ref var otherTransform = ref this.world.GetComponent<Transform>(other);
                    ref var otherCollidedEntities = ref this.world.GetComponent<CollidedEntities>(other);

                    var r = selfRadius.Value + otherRadius.Value;
                    var dx = otherTransform.Position.X - selfTransform.Position.X;
                    var dy = otherTransform.Position.Y - selfTransform.Position.Y;

                    if (dx * dx + dy * dy > r * r) continue;

                    otherCollidedEntities.Entities ??= new Stack<int>(MaxCollisionsPerEntity);

                    if (otherCollidedEntities.Entities.Count < MaxCollisionsPerEntity)
                    {
                        otherCollidedEntities.Entities.Push(self);
                        this.entitiesToClean.Push(other);
                    }

                    if (selfCollidedEntities.Entities.Count < MaxCollisionsPerEntity)
                    {
                        selfCollidedEntities.Entities.Push(other);
                        this.entitiesToClean.Push(self);
                    }
                }
            }
        }
    }
}