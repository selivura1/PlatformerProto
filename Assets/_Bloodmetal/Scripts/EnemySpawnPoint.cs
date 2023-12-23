using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] Enemy _enemy;
        private Enemy _spawned;
        public void Despawn()
        {
            if(_spawned != null )
            {
                Destroy(_spawned.gameObject);
            }
        }
        public void Spawn()
        {
            _spawned =  Instantiate(_enemy, transform);
        }
    }
}
