using TMPro;
using UnityEngine;

namespace Selivura
{
    public class MoneyAmountDisplay : MonoBehaviour
    {
        TMP_Text _text;
        SaveManager _saveManager;
        private void OnEnable()
        {
            _text = GetComponent<TMP_Text>();
            _saveManager = FindAnyObjectByType<SaveManager>();
            _saveManager.OnSaveChanged += Refresh;
            Refresh();
        }
        private void OnDisable()
        {
            _saveManager.OnSaveChanged -= Refresh;
        }
        void Refresh()
        {
            _text.text = "$:" + _saveManager.GetCurrentMoney().ToString();
        }
    }
}
