using Constants;
using Player_Scripts;
using UnityEngine;

namespace Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private CharacterController m_CharacterController;

        private Vector2 _input;
        private Vector3 _rootMotion;
        private float _groundSpeed;
        private float _jumpDamp;
        private Vector3 _velocity;
        private bool _isJumping;
        private bool _isCrouching;
        private float _animatorWeight;

        private void Start()
        {
            _groundSpeed = Player.Instance.PlayerData.m_GroundSpeed;
            _jumpDamp = Player.Instance.PlayerData.m_JumpDamp;
        }

        private void Update()
        {
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");

            m_Animator.SetFloat(AnimatorHashes.InputX, _input.x, 0.1f, Time.deltaTime);
            m_Animator.SetFloat(AnimatorHashes.InputY, _input.y, 0.1f, Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space)) Jump();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                _groundSpeed = Player.Instance.PlayerData.m_GroundMaxSpeed;
                _jumpDamp = Player.Instance.PlayerData.m_MaxJumpDamp;
            }
            else
            {
                _groundSpeed = Player.Instance.PlayerData.m_GroundSpeed;
                _jumpDamp = Player.Instance.PlayerData.m_JumpDamp;
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
            Vector3 stepDownAmunt = Vector3.down * Player.Instance.PlayerData.m_StepDown;

            m_CharacterController.Move(stepForwardAmount + stepDownAmunt);
            _rootMotion = Vector3.zero;

            if (!m_CharacterController.isGrounded) SetInAir(0f);
        }

        private void UpdateInAir()
        {
            _velocity.y -= Player.Instance.PlayerData.m_Gravity * Time.fixedDeltaTime;
            Vector3 displacement = _velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControlle();
            m_CharacterController.Move(displacement);
            _isJumping = !m_CharacterController.isGrounded;
            _rootMotion = Vector3.zero;
            m_Animator.SetBool(AnimatorHashes.IsJumping, _isJumping);
        }

        private Vector3 CalculateAirControlle()
        {
            return ((transform.forward * _input.y) + (transform.right * _input.x)) *
                   (Player.Instance.PlayerData.m_AirControl / 100);
        }

        private void Jump()
        {
            if (!_isJumping)
            {
                float jumpVelocity = Mathf.Sqrt(2f * Player.Instance.PlayerData.m_Gravity *
                                                Player.Instance.PlayerData.m_JumpHeight);
                SetInAir(jumpVelocity);
            }
        }

        private void SetInAir(float jumpVelocity)
        {
            _isJumping = true;
            _velocity = m_Animator.velocity * (_jumpDamp * _groundSpeed);
            _velocity.y = jumpVelocity;
            m_Animator.SetBool(AnimatorHashes.IsJumping, true);
        }

        private void SetAnimationLayerWeight()
        {
            _animatorWeight = Mathf.Lerp(_animatorWeight, _isCrouching ? 1 : 0,
                Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            m_Animator.SetLayerWeight(1, _animatorWeight);
        }
    }
}