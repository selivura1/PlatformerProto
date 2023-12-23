using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Selivura
{
    public class LevelLoader : MonoBehaviour
    {
        private Player _player;
        private PlayerInputHandler _playerInputHandler;
        [SerializeField] private LevelData[] _allLevels;
        public LevelData[] AllLevels => _allLevels;
        public int CurrentLevelIndex { get; private set; } = -2;
        public Level CurrentLevel;
        public delegate void LevelLoadedHandler(int level);
        public event LevelLoadedHandler OnLevelLoaded;
        public const int MAIN_MENU_SCENEID = 1;
        public const int MAIN_MENU_LEVELID = -1;
        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _playerInputHandler = _player.GetComponent<PlayerInputHandler>();
            _player.OnPlayerRestart += () => RestartCurrentLevel();
            LoadMainMenu();
        }
        private void OnDestroy()
        {
            _player.OnPlayerRestart -= () => RestartCurrentLevel();
        }
        public async Awaitable RestartCurrentLevel()
        {
           await LoadLevel(CurrentLevelIndex);
        }
        public async Awaitable LoadLevel(int levelInLoader)
        {
            UnloadCurrentLevel();

            _player.transform.position = Vector3.zero;
            _playerInputHandler.EnableControls = true;
            _player.Initialize();
            CurrentLevelIndex = levelInLoader;

            await SceneManager.LoadSceneAsync(_allLevels[levelInLoader].SceneID, LoadSceneMode.Additive);
            CurrentLevel = FindAnyObjectByType<Level>();
            OnLevelLoaded?.Invoke(levelInLoader);

            Debug.Log($"Loading mission {levelInLoader}");
        }
        public async Awaitable LoadMainMenu()
        {
            UnloadCurrentLevel();
           _player.transform.position = Vector3.zero;
            _playerInputHandler.EnableControls = false;

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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LoadLevel(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                LoadMainMenu();
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                RestartCurrentLevel();
            }
        }
    }
}
