using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Bloodmetal
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _input;
        PlayerCombat _combat;
        Movement _movement;
        PlayerDirection _direction;
        //EntityAttributes _attributes;
        public MainControls Controls { get; private set; }
        public Vector2 DirectionInput { get; private set; }
        public bool Attack { get; private set; } = false;
        public bool AltAttack { get; private set; } = false;
        bool _isGamepad => _input.currentControlScheme.Equals("GAMEPAD");
        private void OnEnable()
        {
            Controls = new MainControls();
            Controls.Enable();
            _direction = GetComponent<PlayerDirection>();
            _input = GetComponent<PlayerInput>();
            _combat = GetComponent<PlayerCombat>();
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
            DirectionInput = Controls.Game.Move.ReadValue<Vector2>();

            _movement.MoveHorizontally(DirectionInput.x);
            _direction.SetDirection(new Vector2(_direction.Direction.x, DirectionInput.y));
            if (Controls.Game.Jump.WasPressedThisFrame())
            {
                _movement.InputJump();
            }
            if(Controls.Game.Jump.WasReleasedThisFrame())
            {
                _movement.ReleaseJump();
            }
            Attack = Controls.Game.Attack.WasPerformedThisFrame();
            AltAttack = Controls.Game.AltAttack.IsPressed();
            if(Attack)
            {
                _combat.MeleeAttack();
            }
            //if (Attack)
            //    _combat.PrimaryAttack(Aim);
            //if(AltAttack)
            //    _combat.SecondaryAttack(Aim);
        }
    }
}