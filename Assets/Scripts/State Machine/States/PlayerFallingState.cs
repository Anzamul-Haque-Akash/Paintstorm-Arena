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
            Player.Instance.m_IsFalling = true;
        }
        
        public override void UpdateState() { }
        
        public override void FixedUpdateState()
        {
            switch (Player.Instance.CharacterController.isGrounded)
            {
                case false:
                    PlayerFalling();
                    Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, true);
                    break;
                case true:
                    Player.Instance.Animator.SetBool(AnimatorHashes.IsJumping, false);
                    Player.Instance.m_IsFalling = false;
                    break;
            }
        }

        private void PlayerFalling()
        {
            Player.Instance.m_Velocity = Player.Instance.Animator.velocity * (Player.Instance.m_JumpDamp * Player.Instance.m_GroundSpeed);
            Player.Instance.m_Velocity.y = 0;
        }
    }
}