using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] Enemy _enemy;
        private Enemy _spawned;
        private Player _player;
        public UnityEvent OnEnemyCleared;
        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _player.OnPlayerRespawn += OnPlayerRespawn;
        }
        private void OnPlayerRespawn()
        {
            Despawn();
        }
        private void Despawn()
        {
            if (_spawned != null)
            {
                Destroy(_spawned.gameObject);
            }
        }
        public void Spawn()
        {
            _spawned = Instantiate(_enemy, transform);
            _spawned.OnKilled.AddListener(() => OnEnemyCleared?.Invoke());
        }
    }
}
