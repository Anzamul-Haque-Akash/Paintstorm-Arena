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
            float jumpVelocity = Mathf.Sqrt(2f * Player.Instance.PlayerData.m_Gravity * _playerStateManager.JumpHeight);
            _playerStateManager.SetInAir(jumpVelocity);
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, true);
        }
        
        private void UpdateInAir()
        {
            _playerStateManager.m_Velocity.y -= Player.Instance.PlayerData.m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = _playerStateManager.m_Velocity * Time.fixedDeltaTime;
            Player.Instance.CharacterController.Move(displacement);
            _playerStateManager.m_IsJumping = !Player.Instance.CharacterController.isGrounded;
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, _playerStateManager.m_IsJumping);
        }
    }
}