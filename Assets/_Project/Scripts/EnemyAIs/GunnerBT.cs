using BehaviorTree;
using System.Collections.Generic;

namespace Selivura
{
    public class GunnerBT : WaypointBT
    {
        public Projectile Projectile;
        public float Speed = 2;
        public float VisisonRange = 5;
        public float AttackCooldown = 1;
        public UnityEngine.LayerMask TargetLayerMask;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new TaskCheckEnemyInRange(transform, VisisonRange, TargetLayerMask),
                    new TaskShoot(transform, Projectile, AttackCooldown),
                }),
                new TaskPatrol(transform, Waypoints, Speed,WaypoinReachDistance)
            });
            return root;
        }
    }
}
