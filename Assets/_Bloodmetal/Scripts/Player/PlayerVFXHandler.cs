using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerVFXHandler : MonoBehaviour
    {
        [SerializeField] VFX _jumpVfx;
        VFXPool _vfxPool;
        PlayerMovement _movement;
        private void Start()
        {
            _vfxPool = VFXPool.instance;
            _movement = GetComponent<PlayerMovement>();
            _movement.OnGroundJump += OnJump;
        }
        private void OnDestroy()
        {
            _movement.OnGroundJump -= OnJump;
        }
        public void OnJump()
        {
            VFX spawned = _vfxPool.GetVFX(_jumpVfx);
            spawned.transform.position = transform.position;
        }

    }
}
