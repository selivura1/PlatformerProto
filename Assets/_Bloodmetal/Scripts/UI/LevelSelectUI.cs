using Selivura;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    public const string COMPLETED_TEXT = "Completed";
    public const string NOT_COMPLETED_TEXT = "Not completed";
    [SerializeField] LevelInfoDisplay _levelInfoDisplayPrefab;
    private List<LevelInfoDisplay> _spawnedDisplays = new List<LevelInfoDisplay>();
    LevelLoader _levelLoader;
    SaveManager _saveManager;
    [SerializeField] Transform _holder;
    private void OnEnable()
    {
        _saveManager = FindAnyObjectByType<SaveManager>();
        _levelLoader = FindAnyObjectByType<LevelLoader>();
        Refresh();
    }
    public void Refresh()
    {
        for (int i = 0; i < _spawnedDisplays.Count; i++)
        {
            Destroy(_spawnedDisplays[i]);
        }
        _spawnedDisplays.Clear();
        for (int i = 0; i < _levelLoader.AllLevels.Length; i++)
        {
            var level = _levelLoader.AllLevels[i];
            var spawned = Instantiate(_levelInfoDisplayPrefab, _holder);
            var levelIndex = i;
            spawned.SetInformation(level.Name, ConvertBoolToCompletionStatus(_saveManager.GetLevelsProgress()[i].Completed));

            spawned.Button.onClick.AddListener(delegate { LoadLevelByIndexButton(levelIndex); });
        }
    }
    private string ConvertBoolToCompletionStatus(bool value)
    {
        return value ? COMPLETED_TEXT : NOT_COMPLETED_TEXT;
    }
    private async void LoadLevelByIndexButton(int index)
    {
        await _levelLoader.LoadLevel(index);
    }
}
