using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Checkpoint : Trigger
    {
        [SerializeField] bool _triggered = false;
        [SerializeField] EnemySpawnPoint[] _spawnPoints;
        public void RespawnEnemies()
        {
            foreach (var item in _spawnPoints)
            {
                item.Despawn();
                item.Spawn();
            }
        }
        protected override void OnTriggered(Player player)
        {
            if(!_triggered)
            {
                _triggered = true;
                player.SetCheckpoint(this);
                RespawnEnemies();
            }
        }
    }
}
