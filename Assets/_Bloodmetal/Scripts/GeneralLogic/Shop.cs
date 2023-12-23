using System;
using System.Collections.Generic;
using UnityEngine;

enum ShopItemType
{
    Weapon,
    HealthUpgrade
}
namespace Selivura
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] List<WeaponItem> _weapons = new List<WeaponItem>();
        [SerializeField] List<HealthUpgrade> _upgrades =new List<HealthUpgrade> {  new HealthUpgrade("Health up", 250, 0), new HealthUpgrade("Health up", 250, 0), new HealthUpgrade("Health up", 250, 0) };
        SaveManager _saveManager;
        Database _database;
        CombatHandler _combat;
        public HealthUpgrade[] Upgrades { get { return _upgrades.ToArray(); } }
        public WeaponItem[] Weapons { get { return _weapons.ToArray(); } }
        private void Awake()
        {
            _combat = FindAnyObjectByType<CombatHandler>();
            _database = FindAnyObjectByType<Database>();
            _saveManager = FindAnyObjectByType<SaveManager>();
            _saveManager.OnSaveChanged += Refresh;

        }
        private void Start()
        {
            Refresh();
        }
        private void OnDestroy()
        {
            _saveManager.OnSaveChanged -= Refresh;
        }
        public void Refresh()
        {
            _weapons.Clear();
            _upgrades.Clear();
            for (int i = 0; i < _database.AllWeapons.Length; i++)
            {
                var weapon = _database.AllWeapons[i];
                var weaponItem = new WeaponItem(weapon.Data.Name, weapon.Data.Price, weapon.Data.WeaponID);
                weaponItem.Owned = _saveManager.GetWeaponUnlocks()[weapon.Data.WeaponID];
                _weapons.Add(weaponItem);
                Debug.Log($"Weapon: {weaponItem.WeaponID} | {weaponItem.Name} | Owned: {weaponItem.Owned}");
            }
            for (int i = 0; i < _database.HealthUpgrades.Count; i++)
            {
                var upgrade = _database.HealthUpgrades[i];
                upgrade.Owned = _saveManager.GetHealthUpgrades()[i];
                _upgrades.Add(upgrade);
                Debug.Log($"Upgrade: {upgrade.UpgradeID} | {upgrade.Name} | Owned: {upgrade.Owned}");
            }
        }
        public bool CheckIfWeaponEquipped(int weaponID)
        {
            return _combat.CheckIfWeaponEquipped(weaponID);
        }
        public void EquipWeapon(int index)
        {
            if(!_combat.CheckIfWeaponEquipped(_weapons[index].WeaponID))
                _combat.SetWeapon(_database.AllWeapons[_weapons[index].WeaponID]);
            Refresh();
        }
        public bool BuyWeapon(int index)
        {
            if (_weapons[index].Owned)
                return false;
            if (_saveManager.GetCurrentMoney() >= _weapons[index].Price)
            {
                _weapons[index].Buy();
                _weapons[index].Owned = true;
                _saveManager.ChangeMoney(-_weapons[index].Price);
                return true;
            }
            else
                return false;
        }
        public bool BuyUpgrade(int index)
        {
            if (_upgrades[index].Owned)
                return false;
            if (_saveManager.GetCurrentMoney() >= _upgrades[index].Price)
            {
                _upgrades[index].Buy();
                _upgrades[index].Owned = true;
                _saveManager.ChangeMoney(-_upgrades[index].Price);
                return true;
            }
            else
                return false;
        }
    }
    [Serializable]
    public abstract class ShopItem
    {
        public string Name = "Item name";
        public int Price = 0;
        public bool Owned = false;
        public ShopItem(string name, int cost)
        {
            Name = name;
            Price = cost;
        }
        public abstract void Buy();

    }
    [Serializable]
    public class WeaponItem : ShopItem
    {
        public int WeaponID;

        public WeaponItem(string name, int cost, int weaponID) : base(name, cost)
        {
            Name = name;
            Price = cost;
            WeaponID = weaponID;
        }
        public override void Buy()
        {
            var saveManager = GameObject.FindAnyObjectByType<SaveManager>();
            saveManager.UnlockWeapon(WeaponID);
            Debug.Log($"Bought weapon {Name} for {Price}");
            Debug.Log($"Money left {saveManager.GetCurrentMoney()}");
        }
    }
    [Serializable]
    public class HealthUpgrade : ShopItem
    {
        public int UpgradeID;

        public HealthUpgrade(string name, int cost, int upgradeID) : base(name, cost)
        {
            Name = name;
            Price = cost;
            UpgradeID = upgradeID;
        }
        public override void Buy()
        {
            var saveManager = GameObject.FindAnyObjectByType<SaveManager>();
            saveManager.UnlockUpgrade(UpgradeID);
            Debug.Log($"Bought upgrade {Name} for {Price}");
        }
    }
}
