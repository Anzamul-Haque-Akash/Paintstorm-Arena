using Constants;
using Player_Scripts;
using UnityEngine;

namespace Controllers
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private CharacterController m_CharacterController;
        [SerializeField] private MovementController m_MovementController;
        
        private Vector2 _input;
        private float _jumpDamp;
        private bool _isJumping;
        private Vector3 _velocity;
        
        private void Start() => _jumpDamp = Player.Instance.PlayerData.m_JumpDamp;
        
        private void Update()
        {
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.Space)) Jump();
            
            _jumpDamp = Input.GetKey(KeyCode.LeftShift) ? Player.Instance.PlayerData.m_MaxJumpDamp : Player.Instance.PlayerData.m_JumpDamp;
            
            m_MovementController.enabled = !_isJumping;
        }
        
        private void FixedUpdate()
        {
            if (_isJumping) UpdateInAir();
        }
        
        private void Jump()
        {
            if (!_isJumping)
            {
                float jumpVelocity = Mathf.Sqrt(2f * Player.Instance.PlayerData.m_Gravity * Player.Instance.PlayerData.m_JumpHeight);
                SetInAir(jumpVelocity);
            }
        }
        
        private void UpdateInAir()
        {
            _velocity.y -= Player.Instance.PlayerData.m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = _velocity * Time.fixedDeltaTime;
            displacement += CalculateAirController();
            m_CharacterController.Move(displacement);
            
            _isJumping = !m_CharacterController.isGrounded;
            
            m_Animator.SetBool(AnimatorHashes.IsJumping, _isJumping);
        }
        
        private void SetInAir(float jumpVelocity)
        {
            _isJumping = true;
            _velocity = m_Animator.velocity * (_jumpDamp * Player.Instance.PlayerData.m_GroundSpeed);
            _velocity.y = jumpVelocity;
            m_Animator.SetBool(AnimatorHashes.IsJumping, true);
        }
        
        private Vector3 CalculateAirController()
        {
            return ((transform.forward * _input.y) + (transform.right * _input.x)) * (Player.Instance.PlayerData.m_AirControl / 100);
        }
    }
}
