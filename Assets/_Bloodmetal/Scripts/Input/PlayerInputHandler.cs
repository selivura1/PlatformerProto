using UnityEngine;
using UnityEngine.InputSystem;
namespace Bloodmetal
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _input;
        //PlayerCombat _combat;
        Movement _movement;
        //EntityAttributes _attributes;
        public MainControls Controls { get; private set; }
        public float HorizontalMovement { get; private set; }
        public bool Attack { get; private set; } = false;
        public bool AltAttack { get; private set; } = false;
        bool _isGamepad => _input.currentControlScheme.Equals("GAMEPAD");
        private void OnEnable()
        {
            Controls = new MainControls();
            Controls.Enable();
            _input = GetComponent<PlayerInput>();
            //_combat = GetComponent<PlayerCombat>();
            _movement = GetComponent<Movement>();
            //_attributes = GetComponent<EntityAttributes>();
        }
        private void OnDisable()
        {
            Controls.Disable();
        }
        private void Update()
        {
            //if (Controls.Game.Pause.WasPressedThisFrame())
            //{
            //    if (!PauseController.GamePaused)
            //        PauseController.PauseGame(true);
            //    else
            //        PauseController.PauseGame(false);
            //}
            //if (PauseController.GamePaused)
            //{
            //    return;
            //}
            RecieveInput();
        }
        private void RecieveInput()
        {
            HorizontalMovement = Controls.Game.Move.ReadValue<Vector2>().x;

            //if (Mathf.Abs(HorizontalMovement) > 0)
            //{
            _movement.MoveHorizontally(HorizontalMovement);
            //if (Dash)
            //    _movement.Dash(Movement);
            //}
            if (Controls.Game.Jump.WasPressedThisFrame())
            {
                _movement.InputJump();
            }
            if(Controls.Game.Jump.WasReleasedThisFrame())
            {
                _movement.ReleaseJump();
            }
            Attack = Controls.Game.Attack.IsPressed();
            AltAttack = Controls.Game.AltAttack.IsPressed();
            //if (Attack)
            //    _combat.PrimaryAttack(Aim);
            //if(AltAttack)
            //    _combat.SecondaryAttack(Aim);
        }
    }
}