using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class Finish : Trigger
    {
        protected override void OnTriggeredPlayer(Player player)
        {
            player.GetComponent<PlayerInputHandler>().EnableControls = false;
            FindAnyObjectByType<LevelCompleteScreenUI>().Show(true);
            FindAnyObjectByType<LevelTimer>().FinishTime = Time.time;
            SaveManager.instance.ChangeMoney(ScoreCounter.Score);
        }
    }
}
