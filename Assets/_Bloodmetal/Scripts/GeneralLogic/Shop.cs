using System;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Shop : MonoBehaviour, IShop
    {
        [SerializeField] List<ShopItem> _items = new List<ShopItem>();
        public ShopItem[] Items { get { return _items.ToArray(); } }

        public void SetItems(ShopItem[] items)
        {
            _items.Clear();
            Debug.Log("Items to sell:");
            for (int i = 0; i < items.Length; i++)
            {
                var item = items[i];
                _items.Add(item);
                Debug.Log($"{item.Name}");
            }
        }
        public bool Buy(int index, int money)
        {
            if (money >= _items[index].Price)
            {
                _items[index].Buy();
                _items.RemoveAt(index);
                return true;
            }
            else
                return false;
        }
    }
    public interface IShop
    {
        public ShopItem[] Items { get; }
        public void SetItems(ShopItem[] items);
        public bool Buy(int index, int money);
    }
    [Serializable]
    public abstract class ShopItem
    {
        public string Name = "Item name";
        public int Price = 0;
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
