using UnityEngine;

namespace Selivura
{
    public class StarPoint : Trigger
    {
        [SerializeField] int _points = 1;
        protected override void OnTriggeredPlayer(Player player)
        {
            base.OnTriggeredPlayer(player); 
            ScoreCounter.AddScore(_points);
            ComboCounter.instance.IncreaseCombo(1);
            gameObject.SetActive(false);
        }
    }
}
