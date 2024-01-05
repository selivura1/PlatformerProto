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
        public WeaponState WeaponState { get; protected set; }
        public float AttackTimer { get; protected set; } = 0;
        protected float cooldownTimer = 0;
        public Vector2 LastAttackDirection { get; protected set; }
        public delegate void AttackingDelegate(Vector2 direction);
        public event AttackingDelegate OnAttack;
        private void Awake()
        {
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
        protected static Vector2 CalculateDirection(Vector2 direction, float spread)
        {
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            angle += Random.Range(-spread, spread);
            return Quaternion.AngleAxis(angle, -Vector3.forward) * Vector2.up;
        }
        public virtual void DoAttackLogic(Vector2 direction)
        {
            direction = CalculateDirection(direction, Data.Spread);
            var hit = Physics2D.Raycast(transform.position, direction, Data.WeaponLength, Data.WallMask);
            float spawnDistance = hit.distance;
            if(spawnDistance <= 0)
            {
                spawnDistance = Data.WeaponLength;
            }
            var spawned = ProjectilePool.instance.GetProjectile(data.BulletPrefab);
            spawned.transform.position = transform.position + (Vector3)(direction * spawnDistance);
            spawned.transform.right = direction;
            spawned.Initialize();
        }
        public abstract void AfterAttackLogic();
    }
}