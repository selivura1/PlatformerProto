using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class Player : MonoBehaviour, IDamageable
    {
        public int Score { get; private set; }
        public float Health { get; private set; } = 12;
        public float MaxHealth { get { return _baseHealth + AdditiveHealth; }}
        [SerializeField] float _baseHealth = 10;
        public float AdditiveHealth = 0;
        private PlayerMovement _movement;
        private Checkpoint _checkpoint;
        private SaveManager _saveManager;
        public delegate void PlayerRespawnDelegate();
        public event PlayerRespawnDelegate OnPlayerRespawn;
        public event PlayerRespawnDelegate OnPlayerRestart;

        public const int HEALTH_UPGRADE_AMOUNT = 5;
        private void Awake()
        {
            _saveManager = FindAnyObjectByType<SaveManager>();
            _movement = GetComponent<PlayerMovement>();
            Initialize();
        }
        public void Initialize()
        {
            Health = MaxHealth;
        }
        //public void Heal(float amount)
        //{
        //    Health += amount;
        //}
        public void TakeDamage(float amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Invoke(nameof(Respawn), 3);
                gameObject.SetActive(false);
            }
        }
        public void SetCheckpoint(Checkpoint checkpoint)
        {
            _checkpoint = checkpoint;
        }
        public void Respawn()
        {
            gameObject.SetActive(true);
            _movement.StopMoving();
            Initialize();
            if (_checkpoint != null)
            {
                transform.position = _checkpoint.transform.position;
                _checkpoint.RespawnEnemies();
                OnPlayerRespawn?.Invoke();
            }
            else
            {
                transform.position = Vector3.zero;
                OnPlayerRestart?.Invoke();
            }
        }
        public void AddScore(int amount)
        {
            Score += amount;
        }
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                Respawn();
            }
        }
    }
}
