using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodmetal
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] CheckBox _leftMeleeHitbox = new CheckBox(Vector2.one, Vector2.zero);
        [SerializeField] CheckBox _rightMeleeHitbox = new CheckBox(Vector2.one, Vector2.zero);
        [SerializeField] CheckBox _topMeleeHitbox = new CheckBox(Vector2.one, Vector2.zero);
        [SerializeField] CheckBox _botMeleeHitbox = new CheckBox(Vector2.one, Vector2.zero);
        [SerializeField] float _deadZone = 0.75f;
        [SerializeField] LayerMask _attackableMask;
        [SerializeField] float _meleeAttackDamage = 1;
        [SerializeField] float _attackCooldown = 0.1f;
        float _lastAttackTime;
        Movement _movement;
        PlayerDirection _direction;
        private void Awake()
        {
            _direction = GetComponent<PlayerDirection>();
            _movement = GetComponent<Movement>();
        }
        private void FixedUpdate()
        {
            _lastAttackTime -= Time.fixedDeltaTime;
        }
        public void MeleeAttack()
        {
            if (_lastAttackTime > 0)
                return;
            _lastAttackTime = _attackCooldown;
            _movement.StopMoving();
            CheckBox _attackHitbox;
            if(_direction.Direction.y > _deadZone)
            {
                _attackHitbox = _topMeleeHitbox;
                Debug.Log("Attacking up");
            }
            else if(_direction.Direction.y < -_deadZone)
            {
                _attackHitbox = _botMeleeHitbox;
                Debug.Log("Attacking down");
            }
            else if (_direction.Direction.x < -_deadZone)
            {
                _attackHitbox = _leftMeleeHitbox;
                Debug.Log($"Attacking left");
            }
            else
            {
                _attackHitbox = _rightMeleeHitbox;
                Debug.Log($"Attacking right");
            }
            var hitTargets = Physics2D.OverlapBoxAll(_attackHitbox.Position + transform.position, _attackHitbox.Size, 0, _attackableMask);
            foreach (var target in hitTargets) 
            { 
                if(target.TryGetComponent(out IDamageable victim))
                {
                    victim.TakeDamage(_meleeAttackDamage);
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.position + _leftMeleeHitbox.Position, _leftMeleeHitbox.Size);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + _rightMeleeHitbox.Position, _rightMeleeHitbox.Size);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position + _topMeleeHitbox.Position, _topMeleeHitbox.Size);

            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(transform.position + _botMeleeHitbox.Position, _botMeleeHitbox.Size);
        }
    }
    [Serializable]
    public class CheckBox
    {
        public Vector3 Size;
        public Vector3 Position;
        public CheckBox(Vector2 size, Vector2 position)
        {
            Size = size;
            Position = position;
        }
    }
}
