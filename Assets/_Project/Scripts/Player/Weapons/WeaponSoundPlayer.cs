using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Weapon))]
    public class WeaponSoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip _shootSound;
        [SerializeField] float _maxPitch = 1.05f, _minPitch = 0.95f;
        AudioSource _audioSource;
        Weapon _weapon;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _weapon = GetComponent<Weapon>();
            _weapon.OnAttack += PlaySound;
        }
        private void OnDestroy()
        {
            _weapon.OnAttack -= PlaySound;
        }
        public void PlaySound(Vector2 dir) 
        {
            if(_audioSource.clip != _shootSound)
                _audioSource.clip = _shootSound;
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.Play();
        }

    }
}
