using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Killzone : Trigger
    {
        protected override void OnTriggered(Player player)
        {
            player.TakeDamage(99999);
        }
    }
}
