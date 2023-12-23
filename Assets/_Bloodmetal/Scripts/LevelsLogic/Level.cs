using System;
using System.Collections;
using System.Collections.Generic;
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
            _currentData = _saveManager.GetLevelsProgress()[_levelLoader.CurrentLevelIndex];
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
        public void CancelLevel()
        {
            // bool[] collectibles = CountCollectibles();
            // DebugMissionResult(collectibles, _currentData.Completed);
            _saveManager.SaveLevelData(new LevelProgressData(_currentData.Completed), _levelLoader.CurrentLevelIndex);
            _levelLoader.LoadMainMenu();
        }
        public void CompleteLevel()
        {
            //bool[] collectibles = CountCollectibles();
            _saveManager.SaveLevelData(new LevelProgressData(true), _levelLoader.CurrentLevelIndex);
          //  DebugMissionResult(collectibles, true);
            _levelLoader.LoadMainMenu();
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
        public bool Completed = false;

        public LevelProgressData(bool completed)
        {
            Completed = completed;
        }
    }
}

