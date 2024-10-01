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
        private Vector3 _velocity;
        private bool _isCrouching;
        private float _animatorWeight;

        private void Start()
        {
            _groundSpeed = Player.Instance.PlayerData.m_GroundSpeed;
        }

        private void Update()
        {
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");

            m_Animator.SetFloat(AnimatorHashes.InputX, _input.x, 0.1f, Time.deltaTime);
            m_Animator.SetFloat(AnimatorHashes.InputY, _input.y, 0.1f, Time.deltaTime);

            _groundSpeed = Input.GetKey(KeyCode.LeftShift)
                ? Player.Instance.PlayerData.m_GroundMaxSpeed
                : Player.Instance.PlayerData.m_GroundSpeed;

            if (Input.GetKeyDown(KeyCode.C)) _isCrouching = !_isCrouching;
            SetAnimationLayerWeight();
        }

        private void OnAnimatorMove()
        {
            _rootMotion += m_Animator.deltaPosition;
        }

        private void FixedUpdate()
        {
            UpdateOnGround();
        }

        private void UpdateOnGround()
        {
            Vector3 stepForwardAmount = _rootMotion * _groundSpeed;

            m_CharacterController.Move(stepForwardAmount);
            _rootMotion = Vector3.zero;
        }

        private void SetAnimationLayerWeight()
        {
            _animatorWeight = Mathf.Lerp(_animatorWeight, _isCrouching ? 1 : 0, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            m_Animator.SetLayerWeight(1, _animatorWeight);
        }
    }
}