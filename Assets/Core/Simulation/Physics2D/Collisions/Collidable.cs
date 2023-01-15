using System.Collections.Generic;

namespace Core.Simulation.Physics2D.Collisions
{
    public struct Collidable
    {
        public Stack<int> CollidedEntities;

        public Collidable(int capacity) => this.CollidedEntities = new Stack<int>(capacity);
    }
}