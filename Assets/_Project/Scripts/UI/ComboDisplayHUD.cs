using TMPro;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(TMP_Text))]
    public class ComboDisplayHUD : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        ComboCounter _comboCounter;
        private void Awake()
        {
            _comboCounter = ComboCounter.instance;
            _comboCounter.OnComboIncreased += UpdateCombo;
            _comboCounter.OnComboReset += ResetCombo;
            _text = GetComponent<TMP_Text>();
        }
        private void OnDestroy()
        {
            _comboCounter.OnComboIncreased -= UpdateCombo;
            _comboCounter.OnComboReset -= ResetCombo;
        }
        private void ResetCombo(int combo)
        {
            _text.text = string.Empty;
        }
        private void UpdateCombo(int combo)
        {
            _text.text = "x" + combo + " " + ConvertRankToString(_comboCounter.CurrentRank);
        }
        public static string ConvertRankToString(Rank rank)
        {
            switch (rank)//Придумать другие
            {
                case Rank.None:
                    return "-";
                case Rank.D:
                    return "Demon";
                case Rank.C:
                    return "Clown";
                case Rank.B:
                    return "BRUH";
                case Rank.A:
                    return "Atrocity";
                case Rank.S:
                    return "Suprimal";
                case Rank.SS:
                    return "SSand";
                case Rank.SSS:
                    return "SSSnake";
                default:
                    return "-";
            }
        }
    }
}
