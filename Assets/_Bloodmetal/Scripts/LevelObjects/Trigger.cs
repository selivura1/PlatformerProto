using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class Trigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                OnTriggered(player);
            }
        }
        protected virtual void OnTriggered(Player player)
        {

        }
    }
}
