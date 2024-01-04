using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu]
    public class WeaponData : ScriptableObject
    {
        public string Name = "Rifle";
        public string Description = "Desc";
        public Vector2 Offset = new Vector2(0,1);
        public float WeaponLength = 1;
        public int WeaponID;
        public bool UnlockedFromStart = false;
        public int Price = 250;
        public float Damage = 1;
        public float AttackDuration = 0.05f;
        public float CooldownTime = 0.2f;
        public float Spread = 5;
        public LayerMask WallMask = 6;
        public Projectile BulletPrefab;
    }
}
