using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.Experimental.GlobalIllumination;

namespace Selivura
{
    public abstract class WaypointBT : Tree
    {
        public UnityEngine.Transform[] Waypoints;
        public float WaypoinReachDistance = 0.1f;
    }
}