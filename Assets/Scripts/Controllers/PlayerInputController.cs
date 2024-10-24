using Constants;
using Player_Scripts;
using State_Machine;
using UnityEngine;

namespace Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private PlayerStateManager m_PlayerStateManager;
        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            m_PlayerStateManager.m_PlayerInput.x = Input.GetAxis("Horizontal");
            m_PlayerStateManager.m_PlayerInput.y = Input.GetAxis("Vertical");

            Player.Instance.Animator.SetFloat(AnimatorHashes.InputX, m_PlayerStateManager.m_PlayerInput.x, 0.1f, Time.deltaTime);
            Player.Instance.Animator.SetFloat(AnimatorHashes.InputY, m_PlayerStateManager.m_PlayerInput.y, 0.1f, Time.deltaTime);
            
            m_PlayerStateManager.SpeedUp(Input.GetKey(KeyCode.LeftShift));

            if (Input.GetKeyDown(KeyCode.C)) Player.Instance.m_IsCrouching = !Player.Instance.m_IsCrouching;
        }
    }
}