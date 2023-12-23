using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class PlayerHealthAmountDisplay : MonoBehaviour
    {
        Player _player;
        TMPro.TMP_Text _text;
        void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _text = GetComponent<TMPro.TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            _text.text = _player.Health.ToString();
        }
    }
}
