using UnityEngine;

namespace Selivura
{
    public class PlayerAnimations : MonoBehaviour
    {
        CombatHandler _combat;
        PlayerMovement _movement;
        Animator _anim;
        [SerializeField] private string _horizontalMovementParamName = "Move";
        [SerializeField] private string _isJumpingParamName = "IsJumping";
        [SerializeField] private string _isFallingParamName = "IsFalling";
        [SerializeField] private string _isShootingParamName = "IsShooting";
        [SerializeField] private string _ShootXParamName = "ShootX";
        [SerializeField] private string _ShootYParamName = "ShootY";
        [SerializeField] private string[] _meleeAttacks = { "Melee_1", "Melee_2" };
        [SerializeField] private string _wallHangingParamName = "IsWallHanging";
        void Awake()
        {
            _anim = GetComponent<Animator>();
            _movement = GetComponent<PlayerMovement>();
            _combat = GetComponent<CombatHandler>();
            _combat.OnMeleeAttack += PlayMeleeAttackAnimation;
        }
        private void OnDestroy()
        {
            _combat.OnMeleeAttack -= PlayMeleeAttackAnimation;
        }
        private void FixedUpdate()
        {
            if (_movement.LastGroundedTime > 0 && !_movement.IsJumping && !_movement.IsWallJumping)
                _anim.SetFloat(_horizontalMovementParamName, Mathf.Abs(_movement.GetCurrentMovementSpeed().x));
            _anim.SetBool(_isJumpingParamName, _movement.IsJumping || _movement.IsWallJumping);
            _anim.SetBool(_isFallingParamName, _movement.IsFalling);
            _anim.SetBool(_isShootingParamName, _combat.IsShooting);

            if(_movement.AllowWalljump)
                _anim.SetBool(_wallHangingParamName, _movement.IsWallHanging);

            if(_combat.IsShooting)
            {
                _anim.SetFloat(_ShootYParamName, _combat.LastAttackDirection.y);
                _anim.SetFloat(_ShootXParamName, _combat.LastAttackDirection.x);
            }
        }
        void PlayMeleeAttackAnimation()
        {
            _anim.Play(_meleeAttacks[Random.Range(0, _meleeAttacks.Length)]);
        }
    }
}
