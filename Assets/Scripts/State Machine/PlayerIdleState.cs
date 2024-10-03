using Player_Scripts;
using UnityEngine;

namespace State_Machine
{
    public class PlayerIdleState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;
        
        public override void EnterState(PlayerStateManager playerStateManager)
        {
            Debug.Log("Player in idle state.");
            _playerStateManager = playerStateManager;
        }

        public override void UpdateState()
        {
            Debug.Log("Player idle state updating.");
            UpdateOnGround();
        }

        public override void FixedUpdateState() { }
        
        private void UpdateOnGround()
        {
            Vector3 stepDownAmunt = Vector3.down * Player.Instance.PlayerData.m_StepDown;

            _playerStateManager.m_CharacterController.Move(stepDownAmunt);
            _playerStateManager.m_RootMotion = Vector3.zero;
        }
    }
}
