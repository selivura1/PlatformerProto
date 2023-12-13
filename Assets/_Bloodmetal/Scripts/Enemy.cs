using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodmetal
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _currentHealth = 100;
        [SerializeField] private float _maxHealth = 100;
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
            Debug.Log($"-{amount} ({_currentHealth}/{_maxHealth})");
        }
    }
}
