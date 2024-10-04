using Constants;
using Player_Scripts;
using UnityEngine;

namespace State_Machine.States
{
    public class PlayerFallingState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;
        public override void EnterState(PlayerStateManager playerStateManager)
        {
            _playerStateManager = playerStateManager;
            _playerStateManager.m_IsFalling = true;
        }

        public override void UpdateState() { }

        public override void FixedUpdateState() => UpdateOnGround();
        
        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _playerStateManager.m_RootMotion * _playerStateManager.GroundSpeed;
            Vector3 stepDownAmunt = Vector3.down * Player.Instance.PlayerData.m_StepDown;
            Player.Instance.CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _playerStateManager.m_RootMotion = Vector3.zero;
            
            if (!Player.Instance.CharacterController.isGrounded) SetInAir(0f);
            if (Player.Instance.CharacterController.isGrounded)
            {
                Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, false);
                _playerStateManager.m_IsFalling = false;
            }
        }
        
        private void SetInAir(float jumpVelocity)
        {
            _playerStateManager.m_Velocity = Player.Instance.Animator.velocity * (_playerStateManager.JumpDamp * _playerStateManager.GroundSpeed);
            _playerStateManager.m_Velocity.y = jumpVelocity;
            Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, true);
        }
    }
}
