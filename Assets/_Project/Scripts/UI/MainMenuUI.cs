using UnityEngine;

namespace Selivura
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject _levelSelectWindow;
        [SerializeField] GameObject _shopWindow;
        [SerializeField] GameObject _equipmentWindow;
        public void ChangeEquipmentVisibility(bool visible)
        {
            _equipmentWindow.SetActive(visible);
        }
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
