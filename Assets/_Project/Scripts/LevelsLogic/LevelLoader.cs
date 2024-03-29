using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class LevelLoader : MonoBehaviour
    {
        public static LevelLoader instance;
        [SerializeField] private LevelData[] _allLevels;
        public LevelData[] AllLevels => _allLevels;
        public int CurrentLevelIndex { get; private set; } = -2;
        public Level CurrentLevel;
        public delegate void LevelLoadedHandler(int level);
        public event LevelLoadedHandler OnLevelLoaded;
        public const int MAIN_MENU_SCENEID = 1;
        public const int MAIN_MENU_LEVELID = -1;
        public const string TEST_LEVEL_NAME = "Level_Test";
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
            _ = LoadMainMenu();
        }
        public async Awaitable RestartCurrentLevel()
        {
            await LoadLevel(CurrentLevelIndex);
        }
        public async Awaitable LoadLevel(int levelInLoader)
        {
            UnloadCurrentLevel();
            CurrentLevelIndex = levelInLoader;
            FindAnyObjectByType<LoadingCover>().Cover();
            await SceneManager.LoadSceneAsync(_allLevels[levelInLoader].SceneID, LoadSceneMode.Additive);
            CurrentLevel = FindAnyObjectByType<Level>();
            OnLevelLoaded?.Invoke(levelInLoader);

            Debug.Log($"Loading level {levelInLoader}");
        }
        public async Awaitable LoadMainMenu()
        {
            UnloadCurrentLevel();

            CurrentLevelIndex = MAIN_MENU_LEVELID;
            await SceneManager.LoadSceneAsync(MAIN_MENU_SCENEID, LoadSceneMode.Additive);

            OnLevelLoaded?.Invoke(MAIN_MENU_LEVELID);
        }

        private void UnloadCurrentLevel()
        {
            if (CurrentLevelIndex != MAIN_MENU_LEVELID)
            {
                if (CurrentLevelIndex != -2)
                    SceneManager.UnloadSceneAsync(_allLevels[CurrentLevelIndex].SceneID);
            }
            else
                SceneManager.UnloadSceneAsync(MAIN_MENU_SCENEID);
            CurrentLevelIndex = -2;
            CurrentLevel = null;
        }

        public static void CloseGame()
        {
            Application.Quit();
        }
        private void LoadTestLevel()
        {
            UnloadCurrentLevel();
            SceneManager.LoadSceneAsync(TEST_LEVEL_NAME,LoadSceneMode.Additive);
            Debug.Log("Test level loaded");
            OnLevelLoaded?.Invoke(-3);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LoadTestLevel();
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _ = LoadMainMenu();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _ = RestartCurrentLevel();
            }
        }
#endif
    }
}
