using System;
using System.Collections;
using UnityEngine;

namespace Selivura
{
    public class CombatHandler : MonoBehaviour
    {
        public bool AllowRangedAttack = true;
        public bool AllowMeleeAttack = true;
        public bool IsShooting
        {
            get
            {
                if (CurrentWeapon != null)
                    return CurrentWeapon.AttackTimer >= -CurrentWeapon.Data.AttackDuration;
                else
                    return false;
            }
        }
        [SerializeField] CheckBox _meleeHitbox = new CheckBox(Vector2.one, Vector2.zero);
        [SerializeField] LayerMask _attackableMask;
        [SerializeField] float _meleeAttackDamage = 1;
        [SerializeField] float _attackCooldown = 0.1f;
        [SerializeField] float _attackCastTime = 0.05f;
        [SerializeField] float _attackEndTime = 0.1f;
        float _lastAttackTime;
        PlayerMovement _movement;
        Player _player;
        [SerializeField] float _punchPower = 15;

        public Action OnMeleeAttack;
        public Weapon CurrentWeapon { get; private set; }
        public delegate void AttackingDelegate(Vector2 direction);
        public event AttackingDelegate OnAttack;
        public Vector2 LastAttackDirection
        {
            get
            {
                if (CurrentWeapon != null)
                    return CurrentWeapon.LastAttackDirection;
                else
                    return Vector2.zero;
            }
        }
    public void SetWeapon(Weapon newWeapon)
        {
            if (CurrentWeapon)
            {
                Destroy(CurrentWeapon.gameObject);
                CurrentWeapon.OnAttack -= InvokeOnAttack;
            }
                var spawned = Instantiate(newWeapon, transform);
            spawned.transform.localPosition = newWeapon.Data.Offset;
            CurrentWeapon = spawned;
            CurrentWeapon.OnAttack += InvokeOnAttack;
        }
        private void InvokeOnAttack(Vector2 direction)
        {
            OnAttack?.Invoke(direction);
        }
        private void Start()
        {
            _movement = GetComponent<PlayerMovement>();
            _player = GetComponent<Player>();
            EquipmentManager.instance.OnEquipped += SetWeapon;
        }
        private void OnDestroy()
        {
            EquipmentManager.instance.OnEquipped -= SetWeapon;
        }
        private void FixedUpdate()
        {
            _lastAttackTime -= Time.fixedDeltaTime;
        }
        private bool CanAttack()
        {
            if (_lastAttackTime > 0)
                return false;
            _lastAttackTime = _attackCooldown;
            return true;
        }
        public void MeleeAttack()
        {
            if (AllowMeleeAttack)
                StartCoroutine(AttackMeleeRoutine());
        }
        IEnumerator AttackMeleeRoutine()
        {
            if (!CanAttack())
                yield return null;
            if (!_movement.IsDashing)
            {
                _movement.StopMoving();
            }
            _movement.LockMovement = true;
            OnMeleeAttack?.Invoke();
            yield return new WaitForSeconds(_attackCastTime);
            var hitTargets = Physics2D.OverlapBoxAll(_meleeHitbox.Position + transform.position, _meleeHitbox.Size, 0, _attackableMask);
            foreach (var target in hitTargets)
            {
                if (target.TryGetComponent(out IDamageable victim))
                {
                    if (_player != (object)victim)
                    {
                        victim.TakeDamage(_meleeAttackDamage);
                        if (target.TryGetComponent(out IPunchable punchy))
                        {
                            punchy.Punch((target.transform.position - (transform.position + _meleeHitbox.Position)) * _punchPower);
                        }
                    }
                }
            }
            _movement.LockMovement = false;
            yield return new WaitForSeconds(_attackEndTime);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + _meleeHitbox.Position, _meleeHitbox.Size);
        }
        public bool CheckIfWeaponEquipped(int weaponID)
        {
            if (CurrentWeapon.Data.WeaponID == weaponID)
                return true;
            return false;
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
