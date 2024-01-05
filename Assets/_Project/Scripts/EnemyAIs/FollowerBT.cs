using BehaviorTree;
using Selivura;
using System.Collections.Generic;
namespace Selivura
{
    public class FollowerBT : WaypointBT
    {
        public Projectile Projectile;
        public float Speed = 2;
        public float VisisonRange = 5;
        public float AttackRange = 3;
        public float AttackCooldown = 1;
        public UnityEngine.LayerMask TargetLayerMask;
        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new TaskCheckEnemyInRange(transform, AttackRange, TargetLayerMask),
                    new TaskShoot(transform, Projectile, AttackCooldown),
                }),
                new Sequence(new List<Node>
                {
                    new TaskCheckEnemyInRange(transform, VisisonRange, TargetLayerMask),
                    new TaskFollowTarget(transform, Speed),
                }),
                new TaskPatrol(transform, Waypoints, Speed, WaypoinReachDistance),
            });
            return root;
        }
    }
}