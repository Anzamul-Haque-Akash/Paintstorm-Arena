using System.Collections.Generic;
using Constants;
using Player_Scripts;
using State_Machine.States;
using UnityEngine;

namespace State_Machine
{
    public class PlayerStateManager : MonoBehaviour
    {
        [field: SerializeField] public float GroundSpeed { get; private set; }
        [field: SerializeField] public float AirSpeed { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public float JumpDamp { get; private set; }
        [field: SerializeField] public bool IsCrouching { get; private set; }
        [field: SerializeField] public float AnimatorWeight { get; private set; }

        public Vector2 m_PlayerInput;
        public Vector3 m_Velocity;
        public Vector3 m_RootMotion;
        public bool m_IsJumping;
        public bool m_IsFalling;
        
        private List<PlayerBaseState> _currentStates;
        
        private readonly PlayerIdleState _playerIdleState = new PlayerIdleState();
        private readonly PlayerMoveState _playerMoveState = new PlayerMoveState();
        private readonly PlayerJumpState _playerJumpState = new PlayerJumpState();
        private readonly PlayerFallingState _playerFallingState = new PlayerFallingState();

        private void Start()
        {
            SpeedUp(false);
            
            _currentStates = new List<PlayerBaseState>();
            _currentStates.Add(_playerIdleState);
            foreach (PlayerBaseState state in _currentStates) state.EnterState(this);
        }
        
        private void SpeedUp(bool flag)
        {
            GroundSpeed = flag ? Player.Instance.PlayerData.m_GroundMaxSpeed : Player.Instance.PlayerData.m_GroundSpeed;
            JumpDamp = flag ? Player.Instance.PlayerData.m_MaxJumpDamp : Player.Instance.PlayerData.m_JumpDamp;
            AirSpeed = flag ? Player.Instance.PlayerData.m_AirMaxSpeed : Player.Instance.PlayerData.m_AirSpeed;
            JumpHeight = flag ? Player.Instance.PlayerData.m_JumpMaxHeight : Player.Instance.PlayerData.m_JumpHeight;
        }

        private void Update()
        {
            GetInput();
            foreach (PlayerBaseState state in _currentStates) state.UpdateState();

            if (!Player.Instance.CharacterController.isGrounded && !m_IsJumping && !m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerFallingState);
                SwitchState(_playerMoveState);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !m_IsJumping && !m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerJumpState);
                SwitchState(_playerMoveState);
            }
            else if (m_PlayerInput == Vector2.zero && !m_IsJumping && !m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerIdleState);
            }
            else if (m_PlayerInput != Vector2.zero && !m_IsJumping && !m_IsFalling)
            {
                _currentStates.Clear();
                SwitchState(_playerMoveState);
            }
            
            SetAnimationLayerWeight();
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

        private void GetInput()
        {
            m_PlayerInput.x = Input.GetAxis("Horizontal");
            m_PlayerInput.y = Input.GetAxis("Vertical");

            Player.Instance.Animator.SetFloat(AnimatorHashes.InputX, m_PlayerInput.x, 0.1f, Time.deltaTime);
            Player.Instance.Animator.SetFloat(AnimatorHashes.InputY, m_PlayerInput.y, 0.1f, Time.deltaTime);

            SpeedUp(Input.GetKey(KeyCode.LeftShift));
            
            if (Input.GetKeyDown(KeyCode.C)) IsCrouching = !IsCrouching;
        }
        
        private void OnAnimatorMove() => m_RootMotion += Player.Instance.Animator.deltaPosition;
        
        private void SetAnimationLayerWeight()
        {
            AnimatorWeight = Mathf.Lerp(AnimatorWeight, IsCrouching ? 1 : 0, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            Player.Instance.Animator.SetLayerWeight(1, AnimatorWeight);
        }
    }
}
