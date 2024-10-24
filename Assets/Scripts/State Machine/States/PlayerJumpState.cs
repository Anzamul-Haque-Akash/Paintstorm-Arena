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
            Player.Instance.m_IsJumping = true;
            Jump();
        }

        public override void UpdateState() { }

        public override void FixedUpdateState()
        {
            if (Player.Instance.m_IsJumping) UpdateInAir();
        }
        
        private void Jump()
        {
            float jumpVelocity = Mathf.Sqrt(2f * Player.Instance.PlayerData.m_Gravity * _playerStateManager.JumpHeight);
            _playerStateManager.SetInAir(jumpVelocity);
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, true);
        }
        
        private void UpdateInAir()
        {
            Player.Instance.m_Velocity.y -= Player.Instance.PlayerData.m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = Player.Instance.m_Velocity * Time.fixedDeltaTime;
            Player.Instance.CharacterController.Move(displacement);
            Player.Instance.m_IsJumping = !Player.Instance.CharacterController.isGrounded;
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, Player.Instance.m_IsJumping);
        }
    }
}