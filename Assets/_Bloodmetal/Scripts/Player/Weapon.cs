using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public enum WeaponState
    {
        Idle,
        Cooldown,
        Attacking,
    }
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField]protected WeaponData data;
        public WeaponData Data { get { return data; } }
        protected Player player;
        protected ProjectilePool projectilePool;
        public WeaponState WeaponState { get; protected set; }
        protected float attackTimer = 0;
        protected float cooldownTimer = 0;
        private void Awake()
        {
            projectilePool = FindAnyObjectByType<ProjectilePool>();
            player = GetComponentInParent<Player>();
        }
        private void FixedUpdate()
        {
            AttackTimer();
            CooldownTimer();
            OnFixedUpdate();
        }
        protected virtual void OnFixedUpdate() { }
        private void AttackTimer()
        {
            if (WeaponState == WeaponState.Attacking)
            {
                attackTimer -= Time.fixedDeltaTime;
                if (attackTimer <= 0)
                {
                    WeaponState = WeaponState.Cooldown;
                    cooldownTimer = data.CooldownTime;
                    AfterAttackLogic();
                }
            }
        }
        private void CooldownTimer()
        {
            if (WeaponState == WeaponState.Cooldown)
            {
                cooldownTimer -= Time.fixedDeltaTime;
                if (cooldownTimer <= 0)
                {
                    WeaponState = WeaponState.Idle;
                }
            }
        }
        public void Attack(Vector2 direction)
        {
            if (WeaponState == WeaponState.Idle)
            {
                WeaponState = WeaponState.Attacking;
                attackTimer = data.AttackDuration;
                DoAttackLogic(direction);
            }
        }
        public virtual void DoAttackLogic(Vector2 direction)
        {
            var spawned = projectilePool.GetProjectile(data.BulletPrefab);
            spawned.transform.position = transform.position;
            spawned.transform.right = direction;
            spawned.Initialize();
        }
        public abstract void AfterAttackLogic();
    }
}