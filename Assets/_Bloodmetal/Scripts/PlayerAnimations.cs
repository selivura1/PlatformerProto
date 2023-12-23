using UnityEngine;

namespace Selivura
{
    public class PlayerAnimations : MonoBehaviour
    {
        CombatHandler _combat;
        PlayerMovement _movement;
        Animator _anim;
        [SerializeField] private string _horizontalMovementParamName = "Move";
        [SerializeField] private string[] _meleeAttacks = { "Melee_1", "Melee_2"};
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
            if(_movement.LastGroundedTime > 0 && !_movement.IsJumping && !_movement.IsWallJumping)
                _anim.SetFloat(_horizontalMovementParamName, Mathf.Abs(_movement.GetCurrentMovementSpeed().x));
        }
        void PlayMeleeAttackAnimation()
        {
            _anim.Play(_meleeAttacks[Random.Range(0, _meleeAttacks.Length)]);
        }
    }
}
