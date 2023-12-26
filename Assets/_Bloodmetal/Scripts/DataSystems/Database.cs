using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Database : MonoBehaviour
    {
        public Weapon[] AllWeapons;
        public Weapon[] EquippableWeapons
        {
            get
            {
                var saveManager = FindAnyObjectByType<SaveManager>();
                var weapons = new List<Weapon>();
                for (int i = 0; i < AllWeapons.Length; i++)
                {
                    if (saveManager.GetWeaponUnlocks()[i])
                        weapons.Add(AllWeapons[i]);
                }
                return weapons.ToArray();
            }
        }
        public Projectile[] AllProjectiles;
        public ScorePoint Coin;
    }
}
