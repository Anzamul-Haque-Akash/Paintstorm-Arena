using UnityEngine;

namespace Player_Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private CharacterController m_CharacterController;
        [SerializeField] private float m_JumpHeight;
        [SerializeField] private float m_Gravity;
        [SerializeField] private float m_SpetDown;
        [SerializeField] private float m_AirControl;
        [SerializeField] private float m_JumpDamp;
        
        private Vector2 _input;
        private Vector3 _rootMotion;
        private Vector3 _velocity;
        private bool _isJumping;
        
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");

        private void Update()
        {
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
            
            m_Animator.SetFloat(InputX, _input.x, 0.1f, Time.deltaTime);
            m_Animator.SetFloat(InputY, _input.y, 0.1f, Time.deltaTime);
            
            if(Input.GetKeyDown(KeyCode.Space)) Jump();
        }
        private void OnAnimatorMove()
        {
            _rootMotion += m_Animator.deltaPosition;
        }
        private void FixedUpdate()
        {
            if (_isJumping)
            {
                _velocity.y -= m_Gravity * Time.fixedDeltaTime;
                Vector3 displacement = _velocity * Time.fixedDeltaTime;
                displacement += CalculateAirControle();
                m_CharacterController.Move(displacement);
                _isJumping = !m_CharacterController.isGrounded;
                _rootMotion = Vector3.zero;
            }
            else
            {
                m_CharacterController.Move(_rootMotion + Vector3.down * m_SpetDown);
                _rootMotion = Vector3.zero;

                if (!m_CharacterController.isGrounded) 
                {
                    _isJumping = true;
                    _velocity = m_Animator.velocity * m_JumpDamp;
                    _velocity.y = 0f;
                }
            }
        }

        private Vector3 CalculateAirControle()
        {
            return ((transform.forward * _input.y) + (transform.right * _input.x)) * (m_AirControl / 100);
        } 

        private void Jump()
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _velocity = m_Animator.velocity * m_JumpDamp;
                _velocity.y = Mathf.Sqrt(2f * m_Gravity * m_JumpHeight);
            }
        }
    }
}
