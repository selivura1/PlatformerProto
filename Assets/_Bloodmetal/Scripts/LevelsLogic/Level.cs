using System;
using UnityEngine;

namespace Selivura
{
    public class Level : MonoBehaviour
    {
        //[SerializeField] Collectible[] _collectibles;
        private LevelLoader _levelLoader;
        //private Database _database;
        private LevelProgressData _currentData;
        private SaveManager _saveManager;
        public void Awake()
        {
            _saveManager = FindAnyObjectByType<SaveManager>();
            _levelLoader = FindAnyObjectByType<LevelLoader>();
            _currentData = _saveManager.GetLevelsProgress(_levelLoader.CurrentLevelIndex);
            //for (int i = 0; i < _currentData.CollectiblesEarned.Length; i++)
            //{
            //    _collectibles[i].Collected = _currentData.CollectiblesEarned[i];
            //}
            //foreach (var collectible in _collectibles)
            //{
            //    if (collectible.Collected)
            //        collectible.gameObject.SetActive(false);
            //}
        }
        public void Quit(bool win)
        {
            LevelProgressData data = new LevelProgressData(_saveManager.GetLevelsProgress(_levelLoader.CurrentLevelIndex).Completed, _saveManager.GetLevelsProgress(_levelLoader.CurrentLevelIndex).Unlocked);
            if (win)
                data = new LevelProgressData(true, true);
            _saveManager.SaveLevelData(data, _levelLoader.CurrentLevelIndex);
            _ = _levelLoader.LoadMainMenu();
        }

        private static void DebugMissionResult(bool[] collectibles, bool completed = false)
        {
            int collected = 0;
            for (int i = 0; i < collectibles.Length; i++)
            {
                if (collectibles[i])
                    collected++;
            }
            Debug.Log("Mission result: " + collected + "/" + collectibles.Length + " | Completed: " + completed);
        }

        //private bool[] CountCollectibles()
        //{
        //    bool[] collectibles = new bool[_collectibles.Length];
        //    for (int i = 0; i < collectibles.Length; i++)
        //    {
        //        collectibles[i] = _collectibles[i].Collected;
        //    }
        //    return collectibles;
        //}
    }
    [Serializable]
    public class LevelProgressData
    {
        public bool Unlocked = false;
        public bool Completed = false;

        public LevelProgressData(bool completed, bool unlocked)
        {
            Unlocked = unlocked;
            Completed = completed;
        }
    }
}

