using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Selivura
{
    public class LoadingCover : MonoBehaviour
    {
        Image _image;
        [SerializeField] float _speed = 15;
        [SerializeField] float _delay = 1;
        private void Awake()
        {
            _image = GetComponent<Image>();
            Cover();
        }
        public void Cover()
        {
            StopAllCoroutines();
            StartCoroutine(ProcessDisappear());
        }
        IEnumerator ProcessDisappear()
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
            yield return new WaitForSeconds(_delay);
            while (_image.color.a > 0)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a - _speed / 100);
                yield return new WaitForSeconds(_speed / 100);
            }
            yield return null;
        }
    }
}
