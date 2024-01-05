using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ScoreCounter : MonoBehaviour
    {
        public static int Score { get; private set; }
        public delegate void ScoreChangedDelegate();
        public static event ScoreChangedDelegate OnScoreChanged;
        private void Awake()
        {
            LevelLoader.instance.OnLevelLoaded += ResetScore;
        }
        private void OnDestroy()
        {
            LevelLoader.instance.OnLevelLoaded -= ResetScore;
        }
        public static void AddScore(int amount)
        {
            Score += amount;
            OnScoreChanged?.Invoke();
        }
        public static void ResetScore(int level = 0)
        {
            Score = 0;
            OnScoreChanged?.Invoke();
        }
    }
}
