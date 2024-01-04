using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailCleaner : MonoBehaviour
    {
        TrailRenderer trailRenderer;
        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }
        void OnDisable()
        {
            trailRenderer.Clear();
        }
    }
}
