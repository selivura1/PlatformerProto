using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] LevelInfoDisplay _infoDisplayPrefab;
        [SerializeField] Transform _holder;
        IShop _shop;
        SaveManager _saveManager;
        private List<LevelInfoDisplay> _displaysSpawned = new List<LevelInfoDisplay>();
        private void Awake()
        {
            _shop = FindAnyObjectByType<Shop>(); // не дает найти по интерфейсу((
            _saveManager = FindAnyObjectByType<SaveManager>();
            _saveManager.OnSaveChanged += Refresh;
        }
        private void OnEnable()
        {
            Refresh();
        }
        private void OnDestroy()
        {
            _saveManager.OnSaveChanged -= Refresh;
        }
        private void ClearList<T>(List<T> list) where T : MonoBehaviour
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }
            list.Clear();
        }
        public void Refresh()
        {
            ClearList(_displaysSpawned);
            for (int i = 0; i < _shop.Items.Length; i++)
            {
                var item = _shop.Items[i];
                var spawned = Instantiate(_infoDisplayPrefab, _holder);
                int itemID = i;

                spawned.SetInformation(item.Name, item.Price.ToString());

                if (_saveManager.GetCurrentMoney() >= item.Price)
                {
                    spawned.Button.onClick.AddListener(delegate { Buy(itemID); });
                }
                else
                {
                    spawned.Button.interactable = false;
                }
                _displaysSpawned.Add(spawned);
            }
        }

        private bool Buy(int itemID)
        {
            if (_shop.Buy(itemID, _saveManager.GetCurrentMoney()))
            {
                _saveManager.ChangeMoney(-_shop.Items[itemID].Price);
                Refresh();
                return true;
            }
            return false;
        }
    }
}
