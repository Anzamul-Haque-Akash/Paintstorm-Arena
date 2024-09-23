using UnityEngine;

namespace Character_Scripts
{
    public class CharacterLocomotion : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;

        private Vector2 _input;
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");

        private void Update()
        {
            _input.x = Input.GetAxis("Horizontal");
            _input.y = Input.GetAxis("Vertical");
            
            m_Animator.SetFloat(InputX, _input.x, 0.1f, Time.deltaTime);
            m_Animator.SetFloat(InputY, _input.y, 0.1f, Time.deltaTime);
        }
    }
}
