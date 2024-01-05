using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class LevelCompleteScreenUI : MonoBehaviour
    {
        [SerializeField] GameObject _screen;
        private void Awake()
        {
            Show(false);
        }
        public void Show(bool value)
        {
            TimeControl.SetPause(value);
            _screen.SetActive(value);
        }
        private void Update()
        {
            if (_screen.activeSelf)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if (LevelLoader.instance.CurrentLevelIndex < LevelLoader.instance.AllLevels.Length)
                    {
                        LevelLoader.instance.LoadLevel(LevelLoader.instance.CurrentLevelIndex + 1);
                        Show(false);
                    }
                    else
                    {
                        LevelLoader.instance.LoadMainMenu();
                        Show(false);
                    }
                }
            }
        }
    }
}
