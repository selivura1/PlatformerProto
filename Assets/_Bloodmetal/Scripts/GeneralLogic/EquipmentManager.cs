using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager instance;
        public List<Weapon> Weapons { get; private set; } = new List<Weapon>();
        public Weapon EquippedWeapon { get; private set; }
        public delegate void EquipmentChangeDelegate(Weapon currentWeapon);
        public event EquipmentChangeDelegate OnEquipped;
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
        public bool EquipWeapon(int id)
        {
            if (id >= Weapons.Count)
                return false;
            if (EquippedWeapon == Weapons[id])
                return false;
            EquippedWeapon = Weapons[id];
            OnEquipped?.Invoke(EquippedWeapon);
            return true;
        }
        public void UpdateAvailableEquipment(Weapon[] weaponsAvailable)
        {
            Weapons.Clear();
            Weapons.AddRange(weaponsAvailable);
        }
    }
}