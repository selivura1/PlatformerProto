using System;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class Enemy : MonoBehaviour, IDamageable, IPunchable, IKillable
    {
        [SerializeField] private float _currentHealth = 100;
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] float _attackCD = 1;
        float _attackTimer = 0;
        private Rigidbody2D _rb;
        public UnityEvent OnKilled { get; set; } = new UnityEvent();

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _rb = GetComponent<Rigidbody2D>();
        }
        public void Heal(float amount)
        {
            _currentHealth += amount;
        }
        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                FindAnyObjectByType<ComboCounter>().IncreaseCombo(1);
                Death();
            }
        }
        public void Move(Vector2 direction, float speed)
        {
            Vector2 targetSpeed = direction * speed;
            Vector2 speedDiff = targetSpeed - _rb.velocity;
            _rb.AddForce(speedDiff, ForceMode2D.Force);
        }
        private void FixedUpdate()
        {
            _attackTimer -= Time.fixedDeltaTime;
        }
        public void Punch(Vector2 direction)
        {
            _rb.AddForce(direction, ForceMode2D.Impulse);
        }
        public void Death()
        {
            OnKilled?.Invoke();
            Destroy(gameObject);
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable victim))
            {
                if (_attackTimer > 0) return;
                victim.TakeDamage(2);
                _attackTimer = 1;
            }
        }

        public void StopMoving()
        {
            _rb.velocity = Vector2.zero;
        }
    }

    public interface IKillable
    {
        public UnityEvent OnKilled { get; set; }
        public void Death();
    }
}
