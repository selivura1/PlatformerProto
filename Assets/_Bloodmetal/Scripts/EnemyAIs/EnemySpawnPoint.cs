using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        protected Enemy spawned;
        protected Player _player;
        public UnityEvent OnEnemyCleared;
        public bool SpawnAutomatically = false;
        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _player.OnPlayerRespawn += OnPlayerRespawn;
            if (SpawnAutomatically)
                Spawn();
        }
        private void OnPlayerRespawn()
        {
            Despawn();
            if (SpawnAutomatically)
                Spawn();
        }
        private void Despawn()
        {
            if (spawned != null)
            {
                Destroy(spawned.gameObject);
            }
        }
        public virtual void Spawn()
        {
            spawned = Instantiate(_enemy, transform);
            spawned.OnKilled.AddListener(() => OnEnemyCleared?.Invoke());
        }
    }
}
