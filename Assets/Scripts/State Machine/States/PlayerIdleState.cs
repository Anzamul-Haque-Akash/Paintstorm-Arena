using Player_Scripts;
using UnityEngine;

namespace State_Machine.States
{
    public class PlayerIdleState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;

        public override void EnterState(PlayerStateManager playerStateManager) => _playerStateManager = playerStateManager;

        public override void UpdateState() => UpdateOnGround();

        public override void FixedUpdateState() { }

        private void UpdateOnGround()
        {
            Vector3 stepDownAmount = Vector3.down * Player.Instance.PlayerData.m_StepDown;
            Player.Instance.CharacterController.Move(stepDownAmount);
            Player.Instance.m_RootMotion = Vector3.zero;
        }
    }
}