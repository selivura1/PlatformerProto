using Pooling;
using UnityEngine;

namespace Selivura
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] PoolingSystem<Projectile> _poolingSystem;
        private void Awake()
        {
            _poolingSystem = new PoolingSystem<Projectile>(transform);
        }
        public Projectile GetProjectile(Projectile prefab)
        {
            return _poolingSystem.Get(prefab);
        }
    }
}
