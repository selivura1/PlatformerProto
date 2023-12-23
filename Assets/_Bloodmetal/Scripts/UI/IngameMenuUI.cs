using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class IngameMenuUI : MonoBehaviour
    {
        Player _player;
        LevelLoader _levelLoader;
        [SerializeField] GameObject _menuRect;
        private void Awake()
        {
            _levelLoader = FindAnyObjectByType<LevelLoader>();
            _player = FindAnyObjectByType<Player>();
        }
        public bool OpenMenu()
        {
            if (_levelLoader.CurrentLevelIndex != LevelLoader.MAIN_MENU_LEVELID)
            {
                _menuRect.SetActive(true);
                TimeControl.SetPause(true);
                return true;
            }
            return false;
        }
        public void ContinueGameButton()
        {
            ContinueGame();
        }
        //Почему-то нельзя выбрать в ивенте кнопки
        public bool ContinueGame()
        {
            if (_levelLoader.CurrentLevelIndex != LevelLoader.MAIN_MENU_LEVELID)
            {
                _menuRect.SetActive(false);
                TimeControl.SetPause(false);
                return true;
            }
            return false;
        }
        public async void ToMainMenu()
        {
            ContinueGame();
            await _levelLoader.LoadMainMenu();
        }
        public async void RestartLevel()
        {
            ContinueGame();
            await _levelLoader.RestartCurrentLevel();
        }
        public void RestartFromCheckpoint()
        {
            _player.Respawn();
            ContinueGame();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_menuRect.activeSelf)
                    OpenMenu();
                else
                    ContinueGame();
            }
        }
    }
}
