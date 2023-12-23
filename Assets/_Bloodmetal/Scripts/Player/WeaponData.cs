using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu]
    public class WeaponData : ScriptableObject
    {
        public string Name = "Rifle";
        public string Description = "Desc";
        public int WeaponID;
        public bool UnlockedFromStart = false;
        public int Price = 250;
        public float Damage = 1;
        public float AttackDuration = 0.05f;
        public float CooldownTime = 0.2f;
        public Projectile BulletPrefab;
    }
}
