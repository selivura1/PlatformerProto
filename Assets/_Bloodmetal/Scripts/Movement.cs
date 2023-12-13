using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bloodmetal
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private float _lastGroundedTime;
        private float _lastJumpInputTime = 0;
        private bool _isJumping = false;
        private bool _isWallJumping = false;
        private bool _jumpInputReleased = true;
        private float _wallJumpStartTime;
        private float _lastWallHangedTime;
        private WallCheckBox _rightWallCheck;
        private WallCheckBox _leftWallCheck;
        [SerializeField] private MovementData _data;
        Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rightWallCheck = new WallCheckBox(_data.RightWallCheck.Size, _data.RightWallCheck.Position);
            _leftWallCheck = new WallCheckBox(_data.LeftWallCheck.Size, _data.LeftWallCheck.Position);
        }
        public void MoveHorizontally(float input)
        {
            if (_isWallJumping)
                return;
            float targetSpeed = _data.MovementSpeed * input;
            float speedDiff = targetSpeed - _rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _data.Acceleration : _data.Deceleration;
            if (_lastGroundedTime < 0)
            {
                accelRate *= _data.AirAccelMult;
            }
            if (Mathf.Abs(_rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(_rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && _lastGroundedTime < 0)
            {
                accelRate = 0;
            }
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, _data.VelocityPower) * Mathf.Sign(speedDiff);
            _rb.AddForce(Vector2.right * movement);
        }
        public void InputJump()
        {
            _lastJumpInputTime = _data.JumpBufferTime;
        }
        public void ReleaseJump()
        {
            Debug.Log("release");
            if (_rb.velocity.y > 0 && _isJumping)
            {
                _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - _data.JumpCutMultiplier), ForceMode2D.Impulse);
            }
            _jumpInputReleased = true;
            _lastJumpInputTime = 0;
        }
        public void Jump()
        {
            if (CanJump())
            {
                _lastJumpInputTime = 0;
                float force = _data.JumpForce;
                if (_rb.velocity.y < 0)
                    force -= _rb.velocity.y;
                _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                _lastGroundedTime = 0;
                _isJumping = true;
                _isWallJumping = false;
                _jumpInputReleased = false;
            }
            else if(CanWallJump())
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
                _wallJumpStartTime = Time.fixedTime;
                _isWallJumping = true;
                _isJumping = false;
            }
        }
        private void ProcessTimers()
        {
            _lastGroundedTime -= Time.fixedDeltaTime;
            _lastWallHangedTime -= Time.fixedDeltaTime;
            _lastJumpInputTime -= Time.fixedDeltaTime;
            _leftWallCheck.LastHangedTime -= Time.fixedDeltaTime;
            _rightWallCheck.LastHangedTime -= Time.fixedDeltaTime;
        }
        private void FixedUpdate()
        {
            ProcessTimers();
            if (Physics2D.OverlapCircle(transform.position + (Vector3)_data.GroundCheckPosition, _data.GroundCheckRadius, _data.GroundLayer))
                _lastGroundedTime = _data.CoyoteTime;
            if (Physics2D.OverlapBox(transform.position + _data.RightWallCheck.Position, _data.RightWallCheck.Size, 0, _data.GroundLayer))
            {
                _rightWallCheck.LastHangedTime = _data.CoyoteTime;
            }
            if (Physics2D.OverlapBox(transform.position + _data.LeftWallCheck.Position, _data.LeftWallCheck.Size, 0, _data.GroundLayer))
            {
                _leftWallCheck.LastHangedTime = _data.CoyoteTime;
            }
            _lastWallHangedTime = Mathf.Max(_leftWallCheck.LastHangedTime, _rightWallCheck.LastHangedTime);
            if(_isJumping && _rb.velocity.y < 0)
            { 
                _isJumping = false;
            }
            if (_isWallJumping && Time.fixedTime - _wallJumpStartTime > _data.WallJumpTime)
            {
                _isWallJumping = false;
            }
            if(_lastJumpInputTime > 0)
            {
                Jump();
            }
            if(_rb.velocity.y < 0)
            {
                _rb.gravityScale = _data.GravityScale * _data.FallGravityMultiplier;
            }
            else
            {
                _rb.gravityScale = _data.GravityScale;
            }

            
        }
        private bool CanJump()
        {
            return _lastGroundedTime > 0 && !_isJumping && !_isWallJumping;
        }
        private bool CanWallJump()
        {
            return !_isWallJumping && _lastWallHangedTime > 0;
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
    public class WallCheckBox
    {
        public Vector3 Size;
        public Vector3 Position;
        public float LastHangedTime = 0;
        public bool IsHanged()
        {
            return LastHangedTime >0;
        }
        public WallCheckBox(Vector2 size, Vector2 position)
        {
            Size = size;
            Position = position;
        }
    }
}

