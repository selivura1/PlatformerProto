using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class LevelTimer : MonoBehaviour
    {
        public float CurrentTime => FinishTime > 0 ? FinishTime - startTime : Time.time - startTime;
        private float startTime;
        public float FinishTime = 0;
        private void Start()
        {
            ResetTimer();
            LevelLoader.instance.OnLevelLoaded += ResetTimer;
        }
        private void OnDestroy()
        {
            LevelLoader.instance.OnLevelLoaded -= ResetTimer;
        }
        private void ResetTimer(int level = 0)
        {
            startTime = Time.time;
            FinishTime = 0;
        }
    }
}
