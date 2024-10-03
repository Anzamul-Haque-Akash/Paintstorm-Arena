using Constants;
using Player_Scripts;
using UnityEngine;

namespace State_Machine
{
    public class PlayerStateManager : MonoBehaviour
    {
        private PlayerBaseState _currentState;
        public PlayerIdleState PlayerIdleState = new PlayerIdleState();
        public PlayerMoveState PlayerMoveState = new PlayerMoveState();
        
        public Animator m_Animator;
        public CharacterController m_CharacterController;

        public Vector2 m_Input;
        public Vector3 m_RootMotion;
        public float m_GroundSpeed;
        public float m_JumpDamp;
        public Vector3 m_Velocity;
        public bool m_IsJumping;
        public bool m_IsCrouching;
        public float m_AnimatorWeight;

        private void Start()
        {
            _currentState = PlayerIdleState;
            _currentState.EnterState(this);
        }

        private void Update()
        {
            GetInput();
            _currentState.UpdateState();

            if (m_Input == Vector2.zero) SwitchState(PlayerIdleState);
            else SwitchState(PlayerMoveState);
        }

        private void FixedUpdate() => _currentState.FixedUpdateState();

        private void SwitchState(PlayerBaseState state)
        {
            _currentState = state;
            state.EnterState(this);
        }

        private void GetInput()
        {
            m_Input.x = Input.GetAxis("Horizontal");
            m_Input.y = Input.GetAxis("Vertical");

            m_Animator.SetFloat(AnimatorHashes.InputX, m_Input.x, 0.1f, Time.deltaTime);
            m_Animator.SetFloat(AnimatorHashes.InputY, m_Input.y, 0.1f, Time.deltaTime);

            //if (Input.GetKeyDown(KeyCode.Space)) Jump();

            SpeedUp(Input.GetKey(KeyCode.LeftShift));
            
            if (Input.GetKeyDown(KeyCode.C)) m_IsCrouching = !m_IsCrouching;
        }
        
        private void OnAnimatorMove()
        {
            m_RootMotion += m_Animator.deltaPosition;
        }

        private void SpeedUp(bool flag)
        {
            m_GroundSpeed = flag ? Player.Instance.PlayerData.m_GroundMaxSpeed : Player.Instance.PlayerData.m_GroundSpeed;
            m_JumpDamp = flag ? Player.Instance.PlayerData.m_MaxJumpDamp : Player.Instance.PlayerData.m_JumpDamp;
        }
    }
}
