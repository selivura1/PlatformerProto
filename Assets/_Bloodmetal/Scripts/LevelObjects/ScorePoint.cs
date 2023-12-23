using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Selivura
{
    public class ScorePoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out Player player))
            {
                player.AddScore(1);
                Destroy(gameObject);
            }
        }
    }
}
