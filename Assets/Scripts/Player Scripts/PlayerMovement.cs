using Unity.VisualScripting;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private CharacterController m_CharacterController;
        [SerializeField] private float m_JumpHeight;
        [SerializeField] private float m_Gravity;
        [SerializeField] private float m_StepDown;
        [SerializeField] private float m_AirControl;
        [SerializeField] private float m_JumpDamp;
        [SerializeField] private float m_MaxJumpDamp;
        [SerializeField] private float m_GroundSpeed;
        [SerializeField] private float m_GroundMaxSpeed;
        [SerializeField] private float m_CrouchSpeed;
        
        private Vector2 _input;
        private Vector3 _rootMotion;
        private float _groundSpeed;
        private float _jumpDamp;
        private Vector3 _velocity;
        private bool _isJumping;
        private bool _isCrouching;
        private float _animatorWeight;
        
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");

        private void Start()
        {
            _groundSpeed = m_GroundSpeed;
            _jumpDamp = m_JumpDamp;
        }

        private void Update()
        {
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
            
            m_Animator.SetFloat(InputX, _input.x, 0.1f, Time.deltaTime);
            m_Animator.SetFloat(InputY, _input.y, 0.1f, Time.deltaTime);
            
            if(Input.GetKeyDown(KeyCode.Space)) Jump();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _groundSpeed = m_GroundMaxSpeed;
                _jumpDamp = m_MaxJumpDamp;
            }
            else
            {
                _groundSpeed = m_GroundSpeed;
                _jumpDamp = m_JumpDamp;
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _isCrouching = !_isCrouching;
            }

            SetAnimationLayerWeight();
        }
        private void OnAnimatorMove()
        {
            _rootMotion += m_Animator.deltaPosition;
        }
        private void FixedUpdate()
        {
            if (_isJumping) UpdateInAir();
            else UpdateOnGround();
        }

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _rootMotion * _groundSpeed;
            Vector3 stepDownAmunt = Vector3.down * m_StepDown;
            
            m_CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _rootMotion = Vector3.zero;

            if (!m_CharacterController.isGrounded) SetInAir(0f);
        }

        private void UpdateInAir()
        {
            _velocity.y -= m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = _velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControlle();
            m_CharacterController.Move(displacement);
            _isJumping = !m_CharacterController.isGrounded;
            _rootMotion = Vector3.zero;
            m_Animator.SetBool(IsJumping, _isJumping);
        }

        private Vector3 CalculateAirControlle()
        {
            return ((transform.forward * _input.y) + (transform.right * _input.x)) * (m_AirControl / 100);
        } 

        private void Jump()
        {
            if (!_isJumping)
            {
                float jumpVelocity = Mathf.Sqrt(2f * m_Gravity * m_JumpHeight);
                SetInAir(jumpVelocity);
            }
        }

        private void SetInAir(float jumpVelocity)
        {
            _isJumping = true;
            _velocity = m_Animator.velocity * (_jumpDamp * _groundSpeed);
            _velocity.y = jumpVelocity;
            m_Animator.SetBool(IsJumping, true);
        }

        private void SetAnimationLayerWeight()
        {
            _animatorWeight = Mathf.Lerp(_animatorWeight, _isCrouching ? 1 : 0, Time.deltaTime * m_CrouchSpeed);
            m_Animator.SetLayerWeight(1,_animatorWeight);
        }
    }
}
