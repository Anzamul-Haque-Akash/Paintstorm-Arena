using Constants;
using Player_Scripts;
using UnityEngine;

namespace State_Machine.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;

        public override void EnterState(PlayerStateManager playerStateManager)
        {
            _playerStateManager = playerStateManager;
            _playerStateManager.m_IsJumping = true;
            Jump();
        }

        public override void UpdateState() { }

        public override void FixedUpdateState()
        {
            if (_playerStateManager.m_IsJumping) UpdateInAir();
        }

        private void Jump()
        {
            float jumpVelocity = Mathf.Sqrt(2f * Player.Instance.PlayerData.m_Gravity * Player.Instance.PlayerData.m_JumpHeight);
            SetInAir(jumpVelocity);
        }

        private void UpdateInAir()
        {
            _playerStateManager.m_Velocity.y -= Player.Instance.PlayerData.m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = _playerStateManager.m_Velocity * Time.fixedDeltaTime;
            displacement += CalculateAirController();
            Player.Instance.CharacterController.Move(displacement);
            _playerStateManager.m_IsJumping = !Player.Instance.CharacterController.isGrounded;
            _playerStateManager.m_RootMotion = Vector3.zero;
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, _playerStateManager.m_IsJumping);
        }

        private Vector3 CalculateAirController()
        {
            return ((_playerStateManager.transform.forward * _playerStateManager.m_PlayerInput.y) +
                    (_playerStateManager.transform.right * _playerStateManager.m_PlayerInput.x)) *
                   (Player.Instance.PlayerData.m_AirControl / 100);
        }

        private void SetInAir(float jumpVelocity)
        {
            _playerStateManager.m_Velocity = Player.Instance.Animator.velocity * (_playerStateManager.JumpDamp * _playerStateManager.GroundSpeed);
            _playerStateManager.m_Velocity.y = jumpVelocity;
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, true);
        }
    }
}