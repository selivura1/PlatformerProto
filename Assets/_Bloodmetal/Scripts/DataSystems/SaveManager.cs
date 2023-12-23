using NUnit.Framework.Internal;
using Selivura;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Selivura
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] SaveFile blankSave;
        public SaveFile BlankSave { get { return blankSave; } }
        [SerializeField] SaveFile save;
        //public Action onSecretUnlock;
        private DataService _dataService = new DataService();
        private const string SaveRelPath = "save.json";
        public bool EnableSaveWriting = true;
        public delegate void SaveChangeDelegate();
        public event SaveChangeDelegate OnSaveChanged;
        private void Awake()
        {
            ReadSaveFromPath(SaveRelPath);
        }
        public LevelProgressData[] GetLevelsProgress()
        {
            return save.LevelProgress;
        }
        public void UnlockLevel(int levelSaveIndex)
        {
            save.LevelUnlockProgress[levelSaveIndex] = true;
            OnSaveChanged?.Invoke();
            WriteSave();
        }
        public bool[] GetLevelUnlockProgress()
        {
            return save.LevelUnlockProgress;
        }
        public void SaveLevelData(LevelProgressData data, int missionID)
        {
            save.LevelProgress[missionID] = data;
            WriteSave();
            Debug.Log($"Saving level data #{missionID}");
        }
        private void ReadSaveFromPath(string path)
        {
            try
            {
                LoadSave(_dataService.LoadData<SaveFile>(path)); 
            }
            catch
            {
                save = blankSave;
            }
        }
        public void LoadSave(SaveFile file)
        {
            save = file;
        }
        private void WriteSave()
        {
            if(EnableSaveWriting)
                _dataService.SaveData(SaveRelPath, save);
            OnSaveChanged?.Invoke();
        }
        public void ResetSave()
        {
            Debug.Log("Save reset...");
            save = blankSave;
            WriteSave();
        }
        public void ChangeMoney(int amount)
        {
            save.Money += amount;
            WriteSave();
        }
        public int GetCurrentMoney()
        {
            return save.Money;
        }
        public void UnlockWeapon(int weaponID)
        {
            save.WeaponsUnlocked[weaponID] = true;
            WriteSave();
        }
        public bool[] GetWeaponUnlocks()
        {
            return save.WeaponsUnlocked;
        }
        public void UnlockUpgrade(int upgradeID)
        {
            save.HealthUpgradesUnlocked[upgradeID] = true;
            WriteSave();
        }

        public bool[] GetHealthUpgrades()
        {
            return save.HealthUpgradesUnlocked;
        }
        public void UnlockWallJump()
        {
            save.WallJumpUnlocked = true;
            WriteSave();
        }
        public void UnlockDash()
        {
            save.DashUnlocked = true;
            WriteSave();
        }
        public bool GetDashUnlocked()
        {
            return save.DashUnlocked;
        }
        public bool GetWallJumpUnlocked()
        {
            return save.WallJumpUnlocked;
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                for (int i = 0; i < save.LevelProgress.Length; i++)
                {
                    Debug.Log($"Level {i}: {save.LevelProgress[i].Completed}");
                }
            if (Input.GetKeyDown(KeyCode.P))
                ResetSave();
            if (Input.GetKeyDown(KeyCode.M))
                ChangeMoney(250);
        }
#endif

        [Serializable]
        public class SaveFile
        {
            public bool WallJumpUnlocked = false;
            public bool DashUnlocked = false;
            public bool[] LevelUnlockProgress = {true, false, false};
            public LevelProgressData[] LevelProgress = {new LevelProgressData(false)};
            public bool[] WeaponsUnlocked = {true, false, false, false};
            public bool[] HealthUpgradesUnlocked = {false, false, false};
            public int Money = 0;
        }
    }
}