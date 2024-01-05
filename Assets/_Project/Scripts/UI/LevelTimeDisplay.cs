using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class LevelTimeDisplay : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text _text;
        public bool UpdateEveryFrame = false;
        
        void OnEnable()
        {
            _text = GetComponent<TMPro.TMP_Text>(); 
            UpdateTimeText();
        }
        private void Update()
        {
            if (UpdateEveryFrame)
                UpdateTimeText();
        }
        private void UpdateTimeText()
        {
            _text.text = "TIME:" + TimeSpan.FromSeconds(FindAnyObjectByType<LevelTimer>().CurrentTime).ToString(@"mm\:ss");
        }
    }
}
