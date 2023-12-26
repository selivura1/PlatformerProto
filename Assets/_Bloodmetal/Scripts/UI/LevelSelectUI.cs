using System.Collections.Generic;
using UnityEngine;
namespace Selivura
{
    public class LevelSelectUI : MonoBehaviour
    {
        public const string COMPLETED_TEXT = "Completed";
        public const string NOT_COMPLETED_TEXT = "Not completed";
        [SerializeField] LevelInfoDisplay _levelInfoDisplayPrefab;
        private List<LevelInfoDisplay> _spawnedDisplays = new List<LevelInfoDisplay>();
        [SerializeField] Transform _holder;
        private void OnEnable()
        {
            Refresh();
        }
        public void Refresh()
        {
            var levelLoader = FindAnyObjectByType<LevelLoader>();
            var saveManager = FindAnyObjectByType<SaveManager>();
            for (int i = 0; i < _spawnedDisplays.Count; i++)
            {
                Destroy(_spawnedDisplays[i].gameObject);
            }
            _spawnedDisplays.Clear();
            for (int i = 0; i < levelLoader.AllLevels.Length; i++)
            {
                var level = levelLoader.AllLevels[i];
                var spawned = Instantiate(_levelInfoDisplayPrefab, _holder);
                var levelIndex = i;
                if (saveManager.GetLevelsProgress(i).Unlocked)
                {
                    spawned.SetInformation(level.Name, ConvertBoolToCompletionStatus(saveManager.GetLevelsProgress(i).Completed));
                    spawned.Button.onClick.AddListener(delegate { LoadLevelByIndexButton(levelLoader, levelIndex); });
                }
                else
                {
                    spawned.Button.interactable = false;
                    spawned.SetInformation("???", "LOCKED");
                }
                _spawnedDisplays.Add(spawned);
            }
        }
        private string ConvertBoolToCompletionStatus(bool value)
        {
            return value ? COMPLETED_TEXT : NOT_COMPLETED_TEXT;
        }
        private async void LoadLevelByIndexButton(LevelLoader levelLoader, int index)
        {
            await levelLoader.LoadLevel(index);
        }
    }
}