using BehaviorTree;

namespace Selivura
{
    public class KnightBT : Tree
    {
        public UnityEngine.Transform[] Waypoints;
        public static float Speed = 2;

        protected override Node SetupTree()
        {
            Node root = new TaskPatrol(transform, Waypoints);
            return root;
        }
    }
}
