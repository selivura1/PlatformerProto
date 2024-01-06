using System;
using TMPro;
using UnityEngine;

namespace Selivura
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager instance;
        [SerializeField] SaveFile blankSave;
        public SaveFile BlankSave { get { return blankSave; } }
        [SerializeField] SaveFile _save;
        //public Action onSecretUnlock;
        private IDataService _dataService = new DataService();
        private const string SAVE_RELATIVE_PATH = "save.bipki";
        private const string LEVEL_RELATIVE_PATH_START = "level";
        private const string LEVEL_RELATIVE_PATH_END = "Progress.bipki";
        public bool EnableSaveWriting = true;
        public delegate void SaveChangeDelegate();
        public event SaveChangeDelegate OnSaveChanged;
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
            ReadSaveFromPath(SAVE_RELATIVE_PATH);
        }
        private void Start()
        {
            EquipmentManager.instance.OnEquipped += (weapon) => SetLastEquippedWeapon(weapon.Data.WeaponID);
        }
        public LevelProgressData GetLevelsProgress(int levelID)
        {
            try
            {
                return _dataService.LoadData<LevelProgressData>(LEVEL_RELATIVE_PATH_START + levelID + LEVEL_RELATIVE_PATH_END);
            }
            catch
            {
                Debug.Log($"No level {levelID} save found. Creating new data...");
                bool unlocked = levelID == 0;
                var newData = new LevelProgressData(false, unlocked);
                if (EnableSaveWriting)
                    SaveLevelData(newData, levelID);
                else
                    Debug.Log("Save writing disabled.");
                return newData;
            }
        }
        public void SaveLevelData(LevelProgressData data, int levelID)
        {
            _dataService.SaveData<LevelProgressData>(LEVEL_RELATIVE_PATH_START + levelID + LEVEL_RELATIVE_PATH_END, data);
            Debug.Log($"Saving level data...");
        }
        private void ReadSaveFromPath(string path)
        {
            try
            {
                LoadSave(_dataService.LoadData<SaveFile>(path));
            }
            catch
            {
                _save = blankSave;
                Debug.Log("Creating new save");
            }
        }
        public void LoadSave(SaveFile file)
        {
            _save = file;
        }
        private void WriteSave()
        {
            if (EnableSaveWriting)
                _dataService.SaveData(SAVE_RELATIVE_PATH, _save);
            else
                Debug.Log("Save writing disabled.");
            OnSaveChanged?.Invoke();
        }
        public void ResetSave()
        {
            for (int i = 0; i < LevelLoader.instance.AllLevels.Length; i++)
            {
                _dataService.DestroyData(LEVEL_RELATIVE_PATH_START + i + LEVEL_RELATIVE_PATH_END);
            }
            Debug.Log("Save reset...");
            _save = blankSave;
            WriteSave();
        }
        public float GetSoundVolume()
        {
            return _save.SoundVolume;
        }
        public void ChangeSoundVolume(float value)
        {
            _save.SoundVolume = value;
            AudioListener.volume = value;
            WriteSave();
        }
        public void ChangeMoney(int amount)
        {
            _save.Money += amount;
            WriteSave();
        }
        public int GetCurrentMoney()
        {
            return _save.Money;
        }
        public void UnlockWeapon(int weaponID)
        {
            _save.WeaponsUnlocked[weaponID] = true;
            WriteSave();
        }
        public bool[] GetWeaponUnlocks()
        {
            return _save.WeaponsUnlocked;
        }
        public void UnlockUpgrade(int upgradeID)
        {
            _save.HealthUpgradesUnlocked[upgradeID] = true;
            WriteSave();
        }

        public bool[] GetHealthUpgrades()
        {
            return _save.HealthUpgradesUnlocked;
        }
        public void UnlockWallJump()
        {
            _save.WallJumpUnlocked = true;
            WriteSave();
        }
        public void UnlockDash()
        {
            _save.DashUnlocked = true;
            WriteSave();
        }
        public bool GetDashUnlocked()
        {
            return _save.DashUnlocked;
        }
        public bool GetWallJumpUnlocked()
        {
            return _save.WallJumpUnlocked;
        }
        public int GetLastEquippedWeapon()
        {
            return _save.LastEquippedWeapon;
        }
        public void SetLastEquippedWeapon(int value)
        {
            _save.LastEquippedWeapon = value;
            WriteSave();
        }
        private void OnApplicationQuit()
        {
            WriteSave();
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                ResetSave();
            if (Input.GetKeyDown(KeyCode.M))
                ChangeMoney(250);
        }
#endif

        [Serializable]
        public class SaveFile
        {
            public int LastEquippedWeapon;
            public float SoundVolume = .5f;
            public bool WallJumpUnlocked = false;
            public bool DashUnlocked = false;
            public bool[] WeaponsUnlocked;
            public bool[] HealthUpgradesUnlocked;
            public int Money;
        }
    }
}