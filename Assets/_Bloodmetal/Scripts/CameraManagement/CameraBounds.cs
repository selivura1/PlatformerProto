using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class CameraBounds : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        private void OnEnable()
        {
            FindAnyObjectByType<CameraManager>().SetCameraBounds(_collider);
        }
    }
}
