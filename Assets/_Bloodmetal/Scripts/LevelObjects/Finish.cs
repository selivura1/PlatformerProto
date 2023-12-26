using UnityEngine;
using UnityEngine.SceneManagement;

namespace Selivura
{
    public class Finish : Trigger
    {
        private float _startTime;
        private void Start()
        {
            _startTime = Time.time;
        }
        protected override void OnTriggeredPlayer(Player player)
        {
            Debug.Log("WIN " + (Time.time - _startTime).ToString("F2"));
            player.GetComponent<PlayerInputHandler>().enabled = false;
            //Invoke(nameof(Restart), 3);
        }
        private void Restart()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
