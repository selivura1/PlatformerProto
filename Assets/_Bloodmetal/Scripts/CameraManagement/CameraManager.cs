using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Selivura
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] _allCams;
        private CinemachineConfiner2D _confiner;

        [SerializeField] private float _fallPanAmount = 0.25f;
        [SerializeField] private float _fallYPanTime = 0.35f;

        public bool IsLerpingYDamping {  get; private set; }
        public bool LerpedFromPlayerFalling { get; private set; }

        private Coroutine _lerpYPanContinue;

        private CinemachineVirtualCamera _currentCam;
        private CinemachineFramingTransposer _framingTransposer;

        private float _normalYPanAmount;

        private void Awake()
        {
            for (int i = 0; i < _allCams.Length; i++)
            {
                if (_allCams[i].enabled)
                {
                    _currentCam = _allCams[i];
                    _framingTransposer = _allCams[i].GetCinemachineComponent<CinemachineFramingTransposer>();
                    _confiner = _allCams[i].GetComponent<CinemachineConfiner2D>();
                }    
            }
            _normalYPanAmount = _framingTransposer.m_YDamping;
        }
        public void SetCameraBounds(Collider2D collider)
        {
            _confiner.m_BoundingShape2D = collider;
        }
        public void LerpYDamping(bool isFalling)
        {
            _lerpYPanContinue = StartCoroutine(LerpYAction(isFalling));
        }
        private IEnumerator LerpYAction(bool isFalling)
        {
            IsLerpingYDamping = true;
            float startDampAmount = _framingTransposer.m_YDamping;
            float endDampAmount = 0;

            if(isFalling)
            { 
                endDampAmount = _fallPanAmount;
                LerpedFromPlayerFalling = true; 
            } 
            else
            {
                endDampAmount = _normalYPanAmount;
            }
            float elapsed = 0;
            while (elapsed < _fallYPanTime) 
            { 
                elapsed += Time.deltaTime;
                float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsed / _fallYPanTime));
                _framingTransposer.m_YDamping = lerpedPanAmount;
                yield return null;
            }

            IsLerpingYDamping = false;
        }
    }
}
