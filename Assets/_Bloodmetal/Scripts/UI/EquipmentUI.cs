using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] LevelInfoDisplay _infoDisplayPrefab;
        [SerializeField] Transform _holder;
        EquipmentManager _equipment;
        SaveManager _saveManager;
        Database _database;
        private List<LevelInfoDisplay> _displaysSpawned = new List<LevelInfoDisplay>();
        private void Awake()
        {
            _database = FindAnyObjectByType<Database>();
            _equipment = FindAnyObjectByType<EquipmentManager>();
            _saveManager = FindAnyObjectByType<SaveManager>();
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
            _equipment.UpdateAvailableEquipment(_database.EquippableWeapons);
            ClearList(_displaysSpawned);
            for (int i = 0; i < _equipment.Weapons.Count; i++)
            {
                var item = _equipment.Weapons[i];
                var spawned = Instantiate(_infoDisplayPrefab, _holder);

                if (item == _equipment.EquippedWeapon)
                {
                    spawned.SetInformation(item.Data.Name, "EQUIPPED");
                    spawned.Button.interactable = false;
                }
                else
                {
                    int weaponID = i;
                    spawned.SetInformation(item.Data.Name, item.Data.Name);
                    spawned.Button.onClick.AddListener(delegate { Equip(weaponID); });
                }
                _displaysSpawned.Add(spawned);
            }
        }
        private bool Equip(int weaponID)
        {
            _equipment.UpdateAvailableEquipment(_database.EquippableWeapons);
            if (_equipment.EquipWeapon(weaponID))
            {
                Refresh();
                return true;
            }
            return false;
        }
    }
}
