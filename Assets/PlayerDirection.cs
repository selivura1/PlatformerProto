using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodmetal
{
    public class PlayerDirection : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
            if (Direction.x < -0.1f)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = Vector3.one;
        }
    }
}
