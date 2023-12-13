using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodmetal
{
    public class BloodCore : MonoBehaviour, IDamageable
    {
        public float Blood { get; private set; } = 10;
        public bool Alive { get { return Blood > 0; } }

        public void Heal(float amount)
        {
            Blood += amount;
        }

        public void TakeDamage(float amount)
        {
            Blood -= amount;
        }
    }
}
