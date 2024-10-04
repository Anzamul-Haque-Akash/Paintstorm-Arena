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
            Vector3 stepForwardAmount = _playerStateManager.m_RootMotion * _playerStateManager.GroundSpeed;
            Vector3 stepDownAmunt = Vector3.down * Player.Instance.PlayerData.m_StepDown;
            Player.Instance.CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _playerStateManager.m_RootMotion = Vector3.zero;
        }
    }
}