using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] float _attackCooldown = 1;
    float _damageAmount = 2;
    float _attackTimer = 0;
    private void FixedUpdate()
    {
        _attackTimer -= Time.fixedDeltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable victim))
        {
            if (_attackTimer > 0) return;
            victim.TakeDamage(_damageAmount);
            _attackTimer = _attackCooldown;
        }
    }
}
