using Player_Scripts;
using UnityEngine;

namespace State_Machine.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;

        public override void EnterState(PlayerStateManager playerStateManager) => _playerStateManager = playerStateManager;

        public override void UpdateState() { }

        public override void FixedUpdateState() => UpdateOnGround();

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = Player.Instance.m_IsJumping ? CalculateAirMove() : CalculateGroundMove();
            
            Vector3 stepDownAmunt = Player.Instance.m_IsJumping
                ? Vector3.zero
                : Vector3.down * Player.Instance.PlayerData.m_StepDown;

            Player.Instance.CharacterController.Move(stepForwardAmount + stepDownAmunt);

            _playerStateManager.m_RootMotion = Vector3.zero;
        }

        private Vector3 CalculateGroundMove()
        {
            return _playerStateManager.m_RootMotion * _playerStateManager.GroundSpeed;
        }

        private Vector3 CalculateAirMove()
        {
            return ((_playerStateManager.transform.forward * _playerStateManager.m_PlayerInput.y) +
                    (_playerStateManager.transform.right * _playerStateManager.m_PlayerInput.x)) *
                   (_playerStateManager.AirSpeed / 100);
        }
    }
}