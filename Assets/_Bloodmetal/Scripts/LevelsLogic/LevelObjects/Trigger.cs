using UnityEngine;
using UnityEngine.Events;

namespace Selivura
{
    public class Trigger : MonoBehaviour
    {
        private Player _player;
        public UnityEvent<Player> OnTriggeredByPlayer;
        public UnityEvent<IDamageable> OnTriggeredByDamageable;
        [SerializeField] private bool _disableOnTrigger;
        private bool _triggered;
        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _player.OnPlayerRespawn += () => _triggered = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_triggered && _disableOnTrigger)
                return;
            if (collision.TryGetComponent(out Player player))
            {
                OnTriggeredByPlayer?.Invoke(player);
                OnTriggeredPlayer(player);
            }
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                OnTriggeredByDamageable?.Invoke(damageable);
                OnTriggeredDamageable(damageable);
            }
            _triggered = true;
        }
        protected virtual void OnTriggeredDamageable(IDamageable damageable)
        {

        }
        protected virtual void OnTriggeredPlayer(Player player)
        {

        }
    }
}
