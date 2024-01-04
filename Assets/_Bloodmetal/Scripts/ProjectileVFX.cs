using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ProjectileVFX : MonoBehaviour
    {
        [SerializeField] private VFX _onDisableVFX;
        private void OnDisable()
        {
            VFXPool.instance.GetVFX(_onDisableVFX).transform.position = transform.position;
        }
    }
}