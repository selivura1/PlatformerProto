using UnityEngine;
using UnityEngine.InputSystem;

namespace Selivura
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInput _input;
        CombatHandler _combat;
        PlayerMovement _movement;
        PlayerDirection _direction;
        //EntityAttributes _attributes;
        public MainControls Controls { get; private set; }
        public Vector2 DirectionInput { get; private set; }
        public bool Attack { get; private set; } = false;
        public bool AltAttack { get; private set; } = false;
        bool _isGamepad => _input.currentControlScheme.Equals("GAMEPAD");
        public bool EnableControls = true;
        private void OnEnable()
        {
            Application.targetFrameRate = 60;
            Controls = new MainControls();
            Controls.Enable();
            _direction = GetComponent<PlayerDirection>();
            _input = GetComponent<PlayerInput>();
            _combat = GetComponent<CombatHandler>();
            _movement = GetComponent<PlayerMovement>();
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
        private void FixedUpdate()
        {
            if (!_combat.IsShooting)
                _movement.MoveHorizontally(DirectionInput.x);
            else
                _movement.MoveHorizontally(0);
        }
        private void RecieveInput()
        {
            if (!EnableControls)
                return;
            DirectionInput = Controls.Game.Move.ReadValue<Vector2>();
            if (Controls.Game.Dash.WasPerformedThisFrame())
                _movement.Dash(Mathf.RoundToInt(DirectionInput.x));
            if (Controls.Game.Jump.WasPressedThisFrame()) //Не менять на IsPressed(), пожалеешь
            {
                _movement.InputJump();
            }
            if (Controls.Game.Jump.WasReleasedThisFrame())
            {
                _movement.ReleaseJump();
            }
            _direction.SetDirection(new Vector2(_direction.Direction.x, DirectionInput.y));

            Attack = Controls.Game.Attack.IsPressed();
            AltAttack = Controls.Game.AltAttack.WasPerformedThisFrame();
            if (AltAttack)
                _combat.MeleeAttack();
            if (Attack)
            {
                if (_isGamepad)
                {
                    if (_combat.CurrentWeapon != null && !(_movement.IsJumping || _movement.IsFalling || _movement.IsWallJumping) )
                    {
                        _combat.CurrentWeapon.Attack(Controls.Game.Aim.ReadValue<Vector2>());
                    }
                }
                else
                {
                    if (_combat.CurrentWeapon != null && _combat.AllowRangedAttack)
                    {
                        _combat.CurrentWeapon.Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition) - _combat.CurrentWeapon.transform.position);
                    }
                }
            }
        }
    }
}