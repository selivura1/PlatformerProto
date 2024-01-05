using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Database : MonoBehaviour
    {
        public static Database instance;
        public Weapon[] AllWeapons;
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
        }
        public Weapon[] EquippableWeapons
        {
            get
            {
                var weapons = new List<Weapon>();
                for (int i = 0; i < AllWeapons.Length; i++)
                {
                    if (SaveManager.instance.GetWeaponUnlocks()[i])
                        weapons.Add(AllWeapons[i]);
                }
                return weapons.ToArray();
            }
        }
        public Projectile[] AllProjectiles;
        public StarPoint Coin;
    }
}
