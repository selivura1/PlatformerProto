using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject _levelSelectWindow;
        [SerializeField] GameObject _shopWindow;
        public void ChangeLevelSelectVisibility(bool visible)
        {
            _levelSelectWindow.SetActive(visible);
        }
        public void ChangeShopVisibility(bool visible)
        {
            _shopWindow.SetActive(visible);
        }
        public void QuitButton()
        {
            Application.Quit();
        }
    }
}
