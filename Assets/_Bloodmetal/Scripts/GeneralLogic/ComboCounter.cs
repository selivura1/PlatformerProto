using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public enum Rank
    {
        None,
        D,
        C,
        B,
        A,
        S,
        SS,
        SSS,
    }
    public class ComboCounter : MonoBehaviour
    {
        public int Combo { get; private set; }
        [SerializeField] float _comboReset = 5;
        private float _comboResetTimer;
        public Rank CurrentRank 
        { 
            get
            {
                switch (Combo)
                {
                    case > 14:
                        return Rank.SSS;
                    case > 12:
                        return Rank.SS;
                    case > 9:
                        return Rank.S;
                    case > 6:
                        return Rank.A;
                    case > 5:
                        return Rank.B;
                    case > 2:
                        return Rank.C;
                    case > 1:
                        return Rank.D;
                    default:
                        return Rank.None;
                }
            } 
        }
        public delegate void ComboChangeHandler(int combo);
        public event ComboChangeHandler OnComboIncreased;
        public event ComboChangeHandler OnComboReset;

        public void IncreaseCombo(int amount)
        {
            Combo += amount;
            _comboResetTimer = _comboReset;
            OnComboIncreased?.Invoke(Combo);
        }
        private void FixedUpdate()
        {
            if (Combo > 0)
            {
                _comboResetTimer -= Time.fixedDeltaTime;
                if (_comboResetTimer < 0)
                {
                    Combo = 0;
                    OnComboReset?.Invoke(Combo);
                }
            }
        }
    }
}
