using Constants;
using Player_Scripts;
using UnityEngine;

namespace State_Machine
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
            _playerStateManager.m_CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _playerStateManager.m_RootMotion = Vector3.zero;
            
            if (!_playerStateManager.m_CharacterController.isGrounded) SetInAir(0f);
            if (_playerStateManager.m_CharacterController.isGrounded)
            {
                _playerStateManager.Animator.SetBool(AnimatorHashes.IsJumping, false);
                _playerStateManager.m_IsFalling = false;
            }
        }
        
        private void SetInAir(float jumpVelocity)
        {
            _playerStateManager.m_Velocity = _playerStateManager.Animator.velocity * (_playerStateManager.JumpDamp * _playerStateManager.GroundSpeed);
            _playerStateManager.m_Velocity.y = jumpVelocity;
            _playerStateManager.Animator.SetBool(AnimatorHashes.IsJumping, true);
        }
    }
}
