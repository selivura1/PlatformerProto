using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class ShootTrigger : MonoBehaviour, IDamageable
    {
        public UnityEvent OnTriggered;
        public bool DisableOnTrigger;
        public bool IsTriggered { get; private set; }
        public void TakeDamage(float amount)
        {
            if (DisableOnTrigger && IsTriggered)
                return;
            IsTriggered = true;
            OnTriggered?.Invoke();
            Debug.Log(name + " " + "triggered!");
        }
    }
}