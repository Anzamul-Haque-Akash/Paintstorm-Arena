using Player_Scripts;
using UnityEngine;

namespace State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;

        public override void EnterState(PlayerStateManager playerStateManager) => _playerStateManager = playerStateManager;

        public override void UpdateState()
        {
            SpeedUp(Player.Instance.m_SpeedUp);
        }

        public override void FixedUpdateState() => UpdateOnGround();

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = Player.Instance.m_IsJumping ? CalculateAirMove() : CalculateGroundMove();
            
            Vector3 stepDownAmunt = Player.Instance.m_IsJumping
                ? Vector3.zero
                : Vector3.down * Player.Instance.PlayerData.m_StepDown;

            Player.Instance.CharacterController.Move(stepForwardAmount + stepDownAmunt);

            Player.Instance.m_RootMotion = Vector3.zero;
        }

        private Vector3 CalculateGroundMove()
        {
            return Player.Instance.m_RootMotion * Player.Instance.m_GroundSpeed;
        }

        private Vector3 CalculateAirMove()
        {
            return ((_playerStateManager.transform.forward * Player.Instance.m_PlayerMoveInput.y) +
                    (_playerStateManager.transform.right * Player.Instance.m_PlayerMoveInput.x)) *
                   (Player.Instance.m_AirSpeed / 100);
        }

        private void SpeedUp(bool flag)
        {
            Player.Instance.m_GroundSpeed = flag ? Player.Instance.PlayerData.m_GroundMaxSpeed : Player.Instance.PlayerData.m_GroundSpeed;
            Player.Instance.m_JumpDamp = flag ? Player.Instance.PlayerData.m_MaxJumpDamp : Player.Instance.PlayerData.m_JumpDamp;
            Player.Instance.m_AirSpeed = flag ? Player.Instance.PlayerData.m_AirMaxSpeed : Player.Instance.PlayerData.m_AirSpeed;
            Player.Instance.m_JumpHeight = flag ? Player.Instance.PlayerData.m_JumpMaxHeight : Player.Instance.PlayerData.m_JumpHeight;
        }
    }
}