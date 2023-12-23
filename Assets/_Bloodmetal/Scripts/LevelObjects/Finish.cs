using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class Finish : Trigger
    {
        protected override void OnTriggered(Player player)
        {
            Debug.Log("WIN");
            player.GetComponent<PlayerInputHandler>().enabled = false;
            Invoke(nameof(Restart), 3);
        }
        private void Restart()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
