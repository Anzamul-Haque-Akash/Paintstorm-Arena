using Constants;
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
            if (_playerStateManager.m_IsJumping) UpdateInAir();
            else UpdateOnGround();
        }

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _playerStateManager.m_RootMotion * _playerStateManager.m_GroundSpeed;
            Vector3 stepDownAmunt = Vector3.down * Player.Instance.PlayerData.m_StepDown;

            _playerStateManager.m_CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _playerStateManager.m_RootMotion = Vector3.zero;

            if (!_playerStateManager.m_CharacterController.isGrounded) SetInAir(0f);
        }

        private void UpdateInAir()
        {
            _playerStateManager.m_Velocity.y -= Player.Instance.PlayerData.m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = _playerStateManager.m_Velocity * Time.fixedDeltaTime;
            displacement += CalculateAirController();
            _playerStateManager.m_CharacterController.Move(displacement);
            _playerStateManager.m_IsJumping = !_playerStateManager.m_CharacterController.isGrounded;
            _playerStateManager.m_RootMotion = Vector3.zero;
            _playerStateManager.m_Animator.SetBool(AnimatorHashes.IsJumping, _playerStateManager.m_IsJumping);
        }

        private Vector3 CalculateAirController()
        {
            return ((_playerStateManager.transform.forward * _playerStateManager.m_Input.y) + (_playerStateManager.transform.right * _playerStateManager.m_Input.x)) *
                   (Player.Instance.PlayerData.m_AirControl / 100);
        }

        private void Jump()
        {
            if (!_playerStateManager.m_IsJumping)
            {
                float jumpVelocity = Mathf.Sqrt(2f * Player.Instance.PlayerData.m_Gravity *
                                                Player.Instance.PlayerData.m_JumpHeight);
                SetInAir(jumpVelocity);
            }
        }

        private void SetInAir(float jumpVelocity)
        {
            _playerStateManager.m_IsJumping = true;
            _playerStateManager.m_Velocity = _playerStateManager.m_Animator.velocity * (_playerStateManager.m_JumpDamp * _playerStateManager.m_GroundSpeed);
            _playerStateManager.m_Velocity.y = jumpVelocity;
            _playerStateManager.m_Animator.SetBool(AnimatorHashes.IsJumping, true);
        }

        private void SetAnimationLayerWeight()
        {
            _playerStateManager.m_AnimatorWeight = Mathf.Lerp(_playerStateManager.m_AnimatorWeight, _playerStateManager.m_IsCrouching ? 1 : 0,
                Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            _playerStateManager.m_Animator.SetLayerWeight(1, _playerStateManager.m_AnimatorWeight);
        }
    }
}
