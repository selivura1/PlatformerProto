using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpawnPoint : EnemySpawnPoint
{
    [SerializeField] private Transform[] Waypoints;
    public override void Spawn()
    {
        base.Spawn();
        spawned.GetComponent<WaypointBT>().Waypoints = Waypoints;
    }
}
