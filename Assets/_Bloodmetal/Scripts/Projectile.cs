using UnityEngine;

namespace Selivura
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _damage;
        [SerializeField] float _speed = 10;
        [SerializeField] float _distance = 10;
        public float Lifetime { get { return _distance / _speed; } }
        Rigidbody2D _rb;
        private float _lifetimeTimer = 0;
        bool _initialized;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        public void Initialize()
        {
            _lifetimeTimer = Lifetime;
            _initialized = true;
        }
        private void Deinitialize()
        {
            _initialized = false;
            gameObject.SetActive(false);
        }
        private void FixedUpdate()
        {
            if (!_initialized)
            {
                return;
            }
            _rb.velocity = transform.right * _speed;
            _lifetimeTimer -= Time.fixedDeltaTime;
            if (_lifetimeTimer < 0)
            {
                Deinitialize();
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(_damage);
            }
            Deinitialize();
        }
    }
}
