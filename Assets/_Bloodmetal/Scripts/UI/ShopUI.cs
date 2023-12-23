using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

namespace Selivura
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] LevelInfoDisplay _infoDisplayPrefab;
        Shop _shop;
        SaveManager _saveManager;
        [SerializeField] Transform _weaponsHolder;
        [SerializeField] Transform _healthUpgradesHolder;
        private List<LevelInfoDisplay> _weaponsDisplays = new List<LevelInfoDisplay>();
        private List<LevelInfoDisplay> _healthDisplays = new List<LevelInfoDisplay>();
        private void Awake()
        {
            _saveManager = FindAnyObjectByType<SaveManager>();
            _shop = FindAnyObjectByType<Shop>();
            _saveManager.OnSaveChanged += RefreshAll;
        }
        private void OnEnable()
        {
            RefreshAll();
        }
        private void OnDestroy()
        {
            _saveManager.OnSaveChanged -= RefreshAll;
        }
        private void ClearList<T>(List<T> list) where T : MonoBehaviour
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }
            list.Clear();
        }
        public void RefreshAll()
        {
            _shop.Refresh();
            RefreshHealthUpgrades();
            RefreshWeapons();
        }
        public void RefreshWeapons()
        {
            ClearList(_weaponsDisplays);
            for (int i = 0; i < _shop.Weapons.Length; i++)
            {
                var weapon = _shop.Weapons[i];
                var spawned = Instantiate(_infoDisplayPrefab, _weaponsHolder);
                var weaponID = weapon.WeaponID;
                if (weapon.Owned)
                {
                    if (_shop.CheckIfWeaponEquipped(weapon.WeaponID))
                    {
                        spawned.SetInformation(weapon.Name, "EQUIPPED");
                        spawned.Button.interactable = false;
                    }
                    else
                    {
                        spawned.Button.onClick.AddListener(delegate { EquipWeapon(weaponID); });
                        spawned.SetInformation(weapon.Name, "OWNED");
                    }
                }
                else
                {
                    spawned.SetInformation(weapon.Name, weapon.Price.ToString());

                    if (_saveManager.GetCurrentMoney() >= weapon.Price)
                    {
                        spawned.Button.onClick.AddListener(delegate { BuyWeapon(weaponID); });
                    }
                    else
                    {
                        spawned.Button.interactable = false;
                    }
                }
                _weaponsDisplays.Add(spawned);
            }
        }
        public void RefreshHealthUpgrades()
        {
            ClearList(_healthDisplays);
            for (int i = 0; i < _shop.Upgrades.Length; i++)
            {
                var upgrade = _shop.Upgrades[i];
                var spawned = Instantiate(_infoDisplayPrefab, _healthUpgradesHolder);
                var upgradeID = upgrade.UpgradeID;
                if (upgrade.Owned)
                {
                    spawned.Button.interactable = false;
                    spawned.SetInformation(upgrade.Name, "OWNED");
                }
                else
                {
                    spawned.SetInformation(upgrade.Name, upgrade.Price.ToString());
                    if (_saveManager.GetCurrentMoney() >= upgrade.Price)
                    {
                        spawned.Button.onClick.AddListener(delegate { BuyUpgrade(upgradeID); });
                    }
                    else
                    {
                        spawned.Button.interactable = false;
                    }
                }
                _healthDisplays.Add(spawned);
            }

        }
        private void EquipWeapon(int weaponID)
        {
            _shop.EquipWeapon(weaponID);
            RefreshAll();
        }
        private void BuyWeapon(int weaponID)
        {
            _shop.BuyWeapon(weaponID);
            RefreshAll();
        }
        private void BuyUpgrade(int upgradeID)
        {
            _shop.BuyUpgrade(upgradeID);
            RefreshAll();
        }
    }
}
