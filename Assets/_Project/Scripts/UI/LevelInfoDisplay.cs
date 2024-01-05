using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura
{
    public class LevelInfoDisplay : MonoBehaviour
    {
        public Button Button;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _bottomText;
        public void SetInformation(string levelName, string status)
        {
            _nameText.text = levelName;
            _bottomText.text = status;
        }
    }
}
