using UnityEngine;

namespace Selivura
{
    public class ScorePoint : Trigger
    {
        [SerializeField] int _points = 1;
        protected override void OnTriggeredPlayer(Player player)
        {
            base.OnTriggeredPlayer(player); 
            player.AddScore(_points);
            gameObject.SetActive(false);
        }
    }
}
