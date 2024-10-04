using Constants;
using Player_Scripts;

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
            switch (Player.Instance.CharacterController.isGrounded)
            {
                case false:
                    SetInAir(0f);
                    Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, true);
                    break;
                case true:
                    Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, false);
                    _playerStateManager.m_IsFalling = false;
                    break;
            }
        }
        
        private void SetInAir(float jumpVelocity)
        {
            _playerStateManager.m_Velocity = Player.Instance.Animator.velocity * (_playerStateManager.JumpDamp * _playerStateManager.GroundSpeed);
            _playerStateManager.m_Velocity.y = jumpVelocity;
        }
    }
}
