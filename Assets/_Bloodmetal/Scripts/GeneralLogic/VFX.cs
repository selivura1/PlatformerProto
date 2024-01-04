using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class VFX : MonoBehaviour
    {
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
