using System.Collections.Generic;
using Player_Scripts;
using State_Machine.States;
using UnityEngine;

namespace State_Machine
{
    public class PlayerStateManager : MonoBehaviour
    {
        private List<PlayerBaseState> _currentStates;

        private readonly PlayerIdleState _playerIdleState = new PlayerIdleState();
        private readonly PlayerMoveState _playerMoveState = new PlayerMoveState();
        private readonly PlayerJumpState _playerJumpState = new PlayerJumpState();
        private readonly PlayerFallingState _playerFallingState = new PlayerFallingState();
        private readonly PlayerReloadState _playerReloadState = new PlayerReloadState();
        
        private void Start()
        {
            Player.Instance.HandPod.SetActive(false);

            _currentStates = new List<PlayerBaseState>();
            _currentStates.Add(_playerIdleState);
            foreach (PlayerBaseState state in _currentStates) state.EnterState(this);
        }

        private void Update()
        {
            foreach (PlayerBaseState state in _currentStates) state.UpdateState();

            if (Player.Instance.m_PlayerMoveInput == Vector2.zero && !Player.Instance.m_IsJumping && !Player.Instance.m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerIdleState);
            }
            if (Player.Instance.m_PlayerMoveInput != Vector2.zero && !Player.Instance.m_IsJumping && !Player.Instance.m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerMoveState);
            }
            if (Player.Instance.m_PlayerJumpInput && !Player.Instance.m_IsJumping && !Player.Instance.m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerJumpState);
                SwitchState(_playerMoveState);
            }
            if (!Player.Instance.CharacterController.isGrounded && !Player.Instance.m_IsJumping && !Player.Instance.m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerFallingState);
                SwitchState(_playerMoveState);
            }
            if (Player.Instance.m_PlayerReloadInput && !Player.Instance.m_IsFalling && !Player.Instance.m_IsJumping && !Player.Instance.m_IsReloading)
            {
                _currentStates.Clear();
                SwitchState(_playerReloadState);
            }
        }

        private void FixedUpdate()
        {
            foreach (PlayerBaseState state in _currentStates) state.FixedUpdateState();
        }

        private void SwitchState(PlayerBaseState state)
        {
            _currentStates.Add(state);
            state.EnterState(this);
        }
        
        private void OnAnimatorMove() => Player.Instance.m_RootMotion += Player.Instance.Animator.deltaPosition;

        public void SetInAir(float jumpVelocity)
        {
            Player.Instance.m_Velocity = Player.Instance.Animator.velocity * (Player.Instance.m_JumpDamp * Player.Instance.m_GroundSpeed);
            Player.Instance.m_Velocity.y = jumpVelocity;
        }
    }
}