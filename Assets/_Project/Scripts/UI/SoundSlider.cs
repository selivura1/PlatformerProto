using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura
{
    public class SoundSlider : MonoBehaviour
    {
        Slider _slider;
        private void OnEnable()
        {
            _slider = GetComponent<Slider>();
            _slider.value = SaveManager.instance.GetSoundVolume();
        }
        public void SetVolume(float value)
        {
            SaveManager.instance.ChangeSoundVolume(value);
        }
    }
}
