using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public List<Weapon> Weapons { get; private set; } = new List<Weapon>();
    public Weapon EquippedWeapon { get; private set; }
    public delegate void EquipmentChangeDelegate(Weapon currentWeapon);
    public event EquipmentChangeDelegate OnEquipped;
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
