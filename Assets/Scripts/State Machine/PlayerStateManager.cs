using Constants;
using Player_Scripts;
using UnityEngine;

namespace State_Machine
{
    public class PlayerStateManager : MonoBehaviour
    {
        [field: SerializeField] public PlayerBaseState CurrentState { get; private set; }
        
        [field: SerializeField] public CharacterController m_CharacterController;
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float GroundSpeed { get; private set; }
        [field: SerializeField] public float JumpDamp { get; private set; }
        [field: SerializeField] public bool IsCrouching { get; private set; }
        [field: SerializeField] public float AnimatorWeight { get; private set; }

        public Vector2 m_PlayerInput;
        public Vector3 m_Velocity;
        public Vector3 m_RootMotion;
        public bool m_IsJumping;
        public bool m_IsFalling;
        
        private readonly PlayerIdleState _playerIdleState = new PlayerIdleState();
        private readonly PlayerMoveState _playerMoveState = new PlayerMoveState();
        private readonly PlayerJumpState _playerJumpState = new PlayerJumpState();
        private readonly PlayerFallingState _playerFallingState = new PlayerFallingState();

        private void Start()
        {
            CurrentState = _playerIdleState;
            CurrentState.EnterState(this);
        }

        private void Update()
        {
            GetInput();
            CurrentState.UpdateState();

            if(!m_CharacterController.isGrounded && !m_IsJumping && !m_IsFalling) SwitchState(_playerFallingState);
            else if (Input.GetKeyDown(KeyCode.Space) && !m_IsJumping && !m_IsFalling)  SwitchState(_playerJumpState);
            else if (m_PlayerInput == Vector2.zero && !m_IsJumping && !m_IsFalling) SwitchState(_playerIdleState);
            else if (m_PlayerInput != Vector2.zero && !m_IsJumping && !m_IsFalling) SwitchState(_playerMoveState);
            
            SetAnimationLayerWeight();
        }

        private void FixedUpdate() => CurrentState.FixedUpdateState();

        private void SwitchState(PlayerBaseState state)
        {
            CurrentState = state;
            state.EnterState(this);
        }

        private void GetInput()
        {
            m_PlayerInput.x = Input.GetAxis("Horizontal");
            m_PlayerInput.y = Input.GetAxis("Vertical");

            Animator.SetFloat(AnimatorHashes.InputX, m_PlayerInput.x, 0.1f, Time.deltaTime);
            Animator.SetFloat(AnimatorHashes.InputY, m_PlayerInput.y, 0.1f, Time.deltaTime);

            SpeedUp(Input.GetKey(KeyCode.LeftShift));
            
            if (Input.GetKeyDown(KeyCode.C)) IsCrouching = !IsCrouching;
        }
        
        private void OnAnimatorMove() => m_RootMotion += Animator.deltaPosition;

        private void SpeedUp(bool flag)
        {
            GroundSpeed = flag ? Player.Instance.PlayerData.m_GroundMaxSpeed : Player.Instance.PlayerData.m_GroundSpeed;
            JumpDamp = flag ? Player.Instance.PlayerData.m_MaxJumpDamp : Player.Instance.PlayerData.m_JumpDamp;
        }
        
        private void SetAnimationLayerWeight()
        {
            AnimatorWeight = Mathf.Lerp(AnimatorWeight, IsCrouching ? 1 : 0, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            Animator.SetLayerWeight(1, AnimatorWeight);
        }
    }
}
