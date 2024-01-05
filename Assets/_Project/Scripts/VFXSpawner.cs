using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpawner : MonoBehaviour
{
    [SerializeField] VFX _toSpawn;
    public bool SpawnOnStart = false;
    private void Start()
    {
        if(SpawnOnStart)
            Spawn();
    }
    public void Spawn()
    {
        var spawned = VFXPool.instance.GetVFX(_toSpawn);
        spawned.transform.position = transform.position;
    }
}
