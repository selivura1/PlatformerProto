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
        [SerializeField] protected WeaponData data;
        public WeaponData Data { get { return data; } }
        protected Player player;
        protected ProjectilePool projectilePool;
        public WeaponState WeaponState { get; protected set; }
        public float AttackTimer { get; protected set; } = 0;
        protected float cooldownTimer = 0;
        public Vector2 LastAttackDirection { get; protected set; }
        public delegate void AttackingDelegate(Vector2 direction);
        public event AttackingDelegate OnAttack;
        private void Awake()
        {
            projectilePool = FindAnyObjectByType<ProjectilePool>();
            player = GetComponentInParent<Player>();
        }
        private void FixedUpdate()
        {
            ProcessAttackTimer();
            CooldownTimer();
            OnFixedUpdate();
        }
        protected virtual void OnFixedUpdate() { }
        private void ProcessAttackTimer()
        {
            AttackTimer -= Time.fixedDeltaTime;
            if (WeaponState == WeaponState.Attacking)
            {
                if (AttackTimer <= 0)
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
                direction.Normalize();
                WeaponState = WeaponState.Attacking;      
                AttackTimer = data.AttackDuration;
                LastAttackDirection = direction;
                OnAttack?.Invoke(direction);
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