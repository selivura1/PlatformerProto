using UnityEngine;

namespace Selivura
{
    public class Door : MonoBehaviour
    {
        [SerializeField] GameObject _block;
        [SerializeField] bool _openByDefault = true;
        Player _player;
        private void Awake()
        {
            _player = FindAnyObjectByType<Player>();
            _player.OnPlayerRespawn += OnPlayerRespawn;
            if (_openByDefault)
                Close(false);
        }
        private void OnDestroy()
        {
            _player.OnPlayerRespawn -= OnPlayerRespawn;
        }
        public void OnPlayerRespawn()
        {
            if (_openByDefault)
                Close(false);
        }
        public void Close(bool value)
        {
            _block.SetActive(value);
        }
    }
}