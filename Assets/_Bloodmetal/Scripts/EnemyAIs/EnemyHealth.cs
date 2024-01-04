using System;
using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
    {
        [SerializeField] private float _currentHealth = 100;
        [SerializeField] private float _maxHealth = 100;
        public UnityEvent OnKilled { get; set; } = new UnityEvent();

        private void Awake()
        {
            _currentHealth = _maxHealth;
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
                ComboCounter.instance.IncreaseCombo(1);
                Death();
            }
        }
        public void Death()
        {
            OnKilled?.Invoke();
            Destroy(gameObject);
        }
    }

    public interface IKillable
    {
        public UnityEvent OnKilled { get; set; }
        public void Death();
    }
}
