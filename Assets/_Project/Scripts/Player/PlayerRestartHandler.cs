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
            _player.OnPlayerRestart += RestartLevel;
            _player.OnPlayerRespawn += OnPlayrRespawn;
        }
        private void OnDisable()
        {
            _levelLoader.OnLevelLoaded -= OnLevelLoaded;
            _player.OnPlayerRestart -= RestartLevel;
            _player.OnPlayerRespawn -= OnPlayrRespawn;
        }
        private void RestartLevel()
        {
            _ = _levelLoader.RestartCurrentLevel();
        }
        private void OnPlayrRespawn()
        {
            ComboCounter.instance.ResetCombo();
            FindAnyObjectByType<LoadingCover>().Cover();
        }
        private void OnLevelLoaded(int level)
        {
            var playerInputHandler = _player.GetComponent<PlayerInputHandler>();
            var equipment = EquipmentManager.instance;
            equipment.UpdateAvailableEquipment(Database.instance.EquippableWeapons);
            equipment.EquipWeapon(SaveManager.instance.GetLastEquippedWeapon());
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
