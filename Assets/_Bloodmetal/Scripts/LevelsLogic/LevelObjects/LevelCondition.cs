using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public abstract class LevelCondition : MonoBehaviour
    {
        public abstract bool ConditionSatisfied { get; }
        public UnityEvent OnConditionMatched;
        protected Player player;
        public abstract void ResetCondition();
        protected virtual void OnAwake() { }
        private void Awake()
        {
            player = FindAnyObjectByType<Player>();
            player.OnPlayerRespawn += ResetCondition;
            OnAwake();
        }
    }
}
