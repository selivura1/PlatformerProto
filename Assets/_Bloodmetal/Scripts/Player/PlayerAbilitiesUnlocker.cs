using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Selivura
{
    public class PlayerAbilitiesUnlocker : MonoBehaviour
    {
        SaveManager _saveManager;
        PlayerMovement _movement;
        Player _player;
        private void Awake()
        {
            _saveManager = FindAnyObjectByType<SaveManager>();
            _movement = GetComponent<PlayerMovement>();
            _player = GetComponent<Player>();
            _player.OnPlayerRespawn += UpdatePlayerAbilities;
            _saveManager.OnSaveChanged += UpdatePlayerAbilities;
            UpdatePlayerAbilities();
        }
        private void OnDestroy()
        {
            _saveManager.OnSaveChanged -= UpdatePlayerAbilities;
            _player.OnPlayerRespawn -= UpdatePlayerAbilities;
        }
        private void UpdatePlayerAbilities()
        {
            _movement.AllowDash = _saveManager.GetDashUnlocked();
            _movement.AllowWalljump = _saveManager.GetWallJumpUnlocked();
            _player.AdditiveHealth = 0;
            foreach (bool unlock in _saveManager.GetHealthUpgrades())
            {
                if (unlock)
                {
                    _player.AdditiveHealth += Player.HEALTH_UPGRADE_AMOUNT;
                }
            }
        }
    }
}
