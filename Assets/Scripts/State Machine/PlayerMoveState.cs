using Player_Scripts;
using UnityEngine;

namespace State_Machine
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;

        public override void EnterState(PlayerStateManager playerStateManager)
        {
            Debug.Log("Player in move state.");
            _playerStateManager = playerStateManager;
        }

        public override void UpdateState()
        {
            Debug.Log("Player in move Upate state.");
            SetAnimationLayerWeight();
        }

        public override void FixedUpdateState()
        {
            UpdateOnGround();
        }

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _playerStateManager.m_RootMotion * _playerStateManager.m_GroundSpeed;
            Vector3 stepDownAmunt = Vector3.down * Player.Instance.PlayerData.m_StepDown;

            _playerStateManager.m_CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _playerStateManager.m_RootMotion = Vector3.zero;
        }

        private void SetAnimationLayerWeight()
        {
            _playerStateManager.m_AnimatorWeight = Mathf.Lerp(_playerStateManager.m_AnimatorWeight,
                _playerStateManager.m_IsCrouching ? 1 : 0, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            _playerStateManager.m_Animator.SetLayerWeight(1, _playerStateManager.m_AnimatorWeight);
        }
    }
}