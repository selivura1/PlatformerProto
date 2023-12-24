using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ShopItemsLoader : MonoBehaviour
    {
        IShop _shop;
        SaveManager _saveManager;
        [SerializeField] WeaponItem[] _weaponsToSell;
        [SerializeField] HealthUpgrade[] _upgradesToSell;
        private void Awake()
        {
            _saveManager = FindAnyObjectByType<SaveManager>();
            _shop = GetComponent<IShop>();
            var items = new List<ShopItem>();
            foreach (var item in _weaponsToSell)
            {
                if (!_saveManager.GetWeaponUnlocks()[item.WeaponID])
                {
                    items.Add(item);
                }
            }
            foreach (var item in _upgradesToSell)
            {
                if (!_saveManager.GetHealthUpgrades()[item.UpgradeID])
                {
                    items.Add(item);
                }
            }
            _shop.SetItems(items.ToArray());
        }
    }
}
