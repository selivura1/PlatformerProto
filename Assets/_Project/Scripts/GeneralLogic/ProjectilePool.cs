using Pooling;
using UnityEngine;

namespace Selivura
{
    public class ProjectilePool : MonoBehaviour
    {
        public static ProjectilePool instance;
        [SerializeField] PoolingSystem<Projectile> _poolingSystem;
        private void Awake()
        {
            if (instance)
            {
                Destroy(this);
                return;
            }
            else
            {
                instance = this;
            }
            _poolingSystem = new PoolingSystem<Projectile>(transform);
        }
        public Projectile GetProjectile(Projectile prefab)
        {
            return _poolingSystem.Get(prefab);
        }
    }
}
