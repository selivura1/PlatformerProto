using UnityEngine;

namespace Selivura
{
    public class PlayerScoreDisplay : MonoBehaviour
    {
        TMPro.TMP_Text _text;
        [SerializeField] private string _prefix = "SCORE:";
        void OnEnable()
        {
            _text = GetComponent<TMPro.TMP_Text>();
            ScoreCounter.OnScoreChanged += UpdateText;
            UpdateText();
        }
        private void OnDisable()
        {
            ScoreCounter.OnScoreChanged -= UpdateText;
        }
        void UpdateText()
        {
            _text.text = _prefix + ScoreCounter.Score.ToString();
        }
    }
}
