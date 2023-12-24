using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class PlayerRestartHandler : MonoBehaviour
    {
        private Player _player;
        private LevelLoader _levelLoader;
        private void OnEnable()
        {
            _levelLoader = FindAnyObjectByType<LevelLoader>();
            _levelLoader.OnLevelLoaded += OnLevelLoaded;

            _player = GetComponent<Player>();
            _player.OnPlayerRestart += () => _levelLoader.RestartCurrentLevel();
        }
        private void OnLevelLoaded(int level)
        {
            var playerInputHandler = _player.GetComponent<PlayerInputHandler>();
            var equipment = FindAnyObjectByType<EquipmentManager>();
            equipment.UpdateAvailableEquipment(FindAnyObjectByType<Database>().EquippableWeapons);
            equipment.EquipWeapon(FindAnyObjectByType<SaveManager>().GetLastEquippedWeapon());
            GetComponent<CombatHandler>().SetWeapon(equipment.EquippedWeapon); 

            _player.transform.position = Vector3.zero;
            playerInputHandler.EnableControls = true;
            _player.Initialize();

            if (level == -1)
            {
                playerInputHandler.EnableControls = false;
            }
        }
    }
}
