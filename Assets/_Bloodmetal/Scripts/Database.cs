using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Database : MonoBehaviour
    {
        public Weapon[] AllWeapons;
        public List<HealthUpgrade> HealthUpgrades = new List<HealthUpgrade>();
        public Projectile[] AllProjectiles;
        public ScorePoint Coin;
    }
}
