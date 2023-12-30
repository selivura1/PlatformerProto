using UnityEngine;

namespace Selivura
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool AllowDash = false;
        public bool AllowWalljump = false;
        public float LastGroundedTime { get; private set; }
        private float _lastJumpInputTime = 0;
        public bool IsJumping { get; private set; } = false;
        public bool IsWallJumping { get; private set; } = false;
        public bool IsDashing { get; private set; } = false;
        private bool _jumpInputReleased = true;
        private float _wallJumpTime;
        private float _lastWallHangedTime;
        private float _dashCooldownTime = 0;
        private float _dashTime = 0;
        private float _jumpTimer = 0;
        private WallCheckBox _rightWallCheck;
        private WallCheckBox _leftWallCheck;
        private PlayerDirection _playerDirection;
        public bool LockMovement = false;
        [SerializeField] private MovementData _data;
        Rigidbody2D _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rightWallCheck = new WallCheckBox(_data.RightWallCheck.Size, _data.RightWallCheck.Position);
            _leftWallCheck = new WallCheckBox(_data.LeftWallCheck.Size, _data.LeftWallCheck.Position);
            _playerDirection = GetComponent<PlayerDirection>();
        }
        public void MoveHorizontally(float input)
        {
            if (IsWallJumping || _dashTime > 0 || LockMovement)
                return;
            float targetSpeed = _data.MovementSpeed * input;
            float speedDiff = targetSpeed - _rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _data.Acceleration : _data.Deceleration;
            if (LastGroundedTime < 0)
            {
                accelRate *= _data.AirAccelMult;
            }
            if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(_rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastGroundedTime < 0)
            {
                accelRate = 0;
            }
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, _data.VelocityPower) * Mathf.Sign(speedDiff);
            _rb.AddForce(Vector2.right * movement);
            if (_lastWallHangedTime < 0 && Mathf.Abs(input) > 0.1f)
                _playerDirection.SetDirection(new Vector2(input, _playerDirection.Direction.y));
        }
        public void Dash(int input)
        {
            if (!AllowDash)
                return;
            if (_dashCooldownTime > 0 || LastGroundedTime > 0 || _dashTime > 0 || LockMovement)
                return;
            if (input == 0)
                input = Mathf.RoundToInt(_playerDirection.Direction.x);
            Debug.Log("Dash");
            StopMoving();
            _dashCooldownTime = _data.DashCooldown;
            _dashTime = _data.DashTime;
            _jumpInputReleased = true;
            IsDashing = true;
            _rb.AddForce(_data.DashForce * Vector2.right * Mathf.RoundToInt(input), ForceMode2D.Impulse);
        }
        public Vector2 GetCurrentMovementSpeed()
        {
            return _rb.velocity;
        }
        public void StopMoving()
        {
            _rb.velocity /= 100;
        }
        public void SlowMoving()
        {
            _rb.velocity /= 3;
        }
        public void InputJump()
        {
            _lastJumpInputTime = _data.JumpBufferTime;
        }
        public void ReleaseJump()
        {
            if (_jumpInputReleased) return;
            if (_rb.velocity.y > 0 && IsJumping)
            {
                _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - _data.JumpCutMultiplier), ForceMode2D.Impulse);
            }
            IsJumping = false;
            _jumpInputReleased = true;
            _lastJumpInputTime = 0;
        }
        public void StopJump()
        {
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - _data.JumpStopMultiplier), ForceMode2D.Impulse);
            _jumpInputReleased = true;
            _lastJumpInputTime = 0;
        }
        public void Jump()
        {
            if (CanJump())
            {
                _lastJumpInputTime = 0;
                float force = _data.JumpForce;
                _rb.velocity.Set(_rb.velocityX, 0);
                _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                LastGroundedTime = 0;
                _jumpTimer = _data.MaxJumpTime;
                IsJumping = true;
                IsWallJumping = false;
                _jumpInputReleased = false;
            }
            else if (CanWallJump())
            {
                _rb.velocity = Vector2.zero;
                if (_rightWallCheck.IsHanged())
                {
                    _rb.AddForce(new Vector2(-_data.WallJumpDirection.x, _data.WallJumpDirection.y), ForceMode2D.Impulse);
                }
                else if (_leftWallCheck.IsHanged())
                {
                    _rb.AddForce(new Vector2(_data.WallJumpDirection.x, _data.WallJumpDirection.y), ForceMode2D.Impulse);
                }
                _wallJumpTime = _data.WallJumpTime;
                IsWallJumping = true;
                IsJumping = false;
            }
        }
        private void ProcessTimers()
        {
            LastGroundedTime -= Time.fixedDeltaTime;
            _lastWallHangedTime -= Time.fixedDeltaTime;
            _lastJumpInputTime -= Time.fixedDeltaTime;
            _leftWallCheck.LastHangedTime -= Time.fixedDeltaTime;
            _rightWallCheck.LastHangedTime -= Time.fixedDeltaTime;
            _dashCooldownTime -= Time.fixedDeltaTime;
            _dashTime -= Time.fixedDeltaTime;
            _jumpTimer -= Time.fixedDeltaTime;
            _wallJumpTime -= Time.fixedDeltaTime;
        }
        private void FixedUpdate()
        {
            ProcessTimers();
            GroundCheck();
            WallCheck();
            JumpCheck();
            FallCheck();
            DashChecks();
        }

        private void GroundCheck()
        {
            if (Physics2D.OverlapCircle(transform.position + (Vector3)_data.GroundCheckPosition, _data.GroundCheckRadius, _data.GroundLayer))
            {
                LastGroundedTime = _data.CoyoteTime;
                _dashCooldownTime = 0;
            }
        }

        private void WallCheck()
        {
            if (Physics2D.OverlapBox(transform.position + _data.RightWallCheck.Position, _data.RightWallCheck.Size, 0, _data.WallJumpLayer))
            {
                _rightWallCheck.LastHangedTime = _data.CoyoteTime;
            }
            if (Physics2D.OverlapBox(transform.position + _data.LeftWallCheck.Position, _data.LeftWallCheck.Size, 0, _data.WallJumpLayer))
            {
                _leftWallCheck.LastHangedTime = _data.CoyoteTime;
            }
            _lastWallHangedTime = Mathf.Max(_leftWallCheck.LastHangedTime, _rightWallCheck.LastHangedTime);
            //if (_rightWallCheck.LastHangedTime > 0)
            //{
            //    _playerDirection.SetDirection(new Vector2(-1, _playerDirection.Direction.y));
            //}
            //else if (_leftWallCheck.LastHangedTime > 0)
            //{
            //    _playerDirection.SetDirection(new Vector2(1, _playerDirection.Direction.y));
            //}
        }

        private void JumpCheck()
        {
            if (IsJumping)
            {
                if(_rb.velocity.y <= 0.01)
                {
                    IsJumping = false;
                }
                if(_jumpTimer <= 0)
                {
                    StopJump();
                }
            }
            if (IsWallJumping && _wallJumpTime < 0)
            {
                IsWallJumping = false;
                StopJump();
            }
            if (_lastJumpInputTime > 0)
            {
                Jump();
            }
        }

        private void FallCheck()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = _data.GravityScale * _data.FallGravityMultiplier;
            }
            else
            {
                _rb.gravityScale = _data.GravityScale;
            }
        }

        private void DashChecks()
        {
            if (IsDashing)
            {
                _rb.gravityScale = 0;
            }
            if (IsDashing && _dashTime < 0)
            {
                IsDashing = false;
                SlowMoving();
            }
        }
        private bool CanJump()
        {
            return LastGroundedTime > 0 && !IsJumping && !IsWallJumping && !LockMovement;
        }
        private bool CanWallJump()
        {
            return !IsWallJumping && _lastWallHangedTime > 0 && !LockMovement && AllowWalljump;
        }
        private void OnDrawGizmosSelected()
        {
            if (!_data) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + (Vector3)_data.GroundCheckPosition, _data.GroundCheckRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + _data.RightWallCheck.Position, _data.RightWallCheck.Size);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + _data.LeftWallCheck.Position, _data.LeftWallCheck.Size);
        }
    }
    [System.Serializable]
    public class WallCheckBox : CheckBox
    {
        public float LastHangedTime = 0;
        public WallCheckBox(Vector2 size, Vector2 position) : base(size, position)
        {

        }
        public bool IsHanged()
        {
            return LastHangedTime > 0;
        }
    }
}

