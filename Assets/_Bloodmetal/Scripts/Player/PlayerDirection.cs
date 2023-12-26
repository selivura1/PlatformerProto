using UnityEngine;

namespace Selivura
{
    public class PlayerDirection : MonoBehaviour
    {
        public Vector2 Direction { get; private set; }
        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
            if (Direction.x < -0.1f)
                transform.localRotation = Quaternion.Euler(new Vector3(0, -180, 0));
            else
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
