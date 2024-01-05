using BehaviorTree;

namespace Selivura
{
    public class WandererBT : WaypointBT
    {
        public float Speed = 2;
        protected override Node SetupTree()
        {
            Node root = new TaskPatrol(transform, Waypoints, Speed, WaypoinReachDistance);
            return root;
        }
    }
}
