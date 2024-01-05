using UnityEngine;

namespace Selivura
{
    [CreateAssetMenu()]
    public class MovementData : ScriptableObject
    {
        public float MovementSpeed = 10;
        public float Acceleration = 3;
        public float Deceleration = 3;
        public float AirAccelMult = 0.25f;
        public float VelocityPower = 1;
        public float MidAirMovementSpeed = 5;

        public float MaxJumpTime = .5f;
        public float JumpForce = 7; //7 > прыжок в ~2.5 тайла
        public float JumpBufferTime = 0.2f;
        public float GravityScale = 1;
        public float FallGravityMultiplier = 2f;
        public float CoyoteTime = 0.16f;
        public float JumpCutMultiplier = 0.25f;
        public float JumpStopMultiplier = 0.1f;
        public float DashCooldown = 1;
        public float DashForce = 10;
        public float DashTime = 0.25f;

        public LayerMask GroundLayer;
        public LayerMask WallJumpLayer = 64;
        public Vector2 GroundCheckPosition = new Vector2(0, -0.95f);
        public float GroundCheckRadius = 0.1f;
        public float WallJumpTime = 0.25f;
        public Vector2 WallJumpDirection = new Vector2(10, 5);
        public WallCheckBox RightWallCheck = new WallCheckBox(new Vector2(0.1f, 1.2f), new Vector2(0.25f, -0.25f));
        public WallCheckBox LeftWallCheck = new WallCheckBox(new Vector2(0.1f, 1.2f), new Vector2(-0.25f, -0.25f));

    }
}
