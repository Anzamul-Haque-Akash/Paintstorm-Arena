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
            Player.Instance.m_PlayerMoveInput.x = Input.GetAxis("Horizontal");
            Player.Instance.m_PlayerMoveInput.y = Input.GetAxis("Vertical");
            Player.Instance.Animator.SetFloat(AnimatorHashes.InputX, Player.Instance.m_PlayerMoveInput.x, 0.1f, Time.deltaTime);
            Player.Instance.Animator.SetFloat(AnimatorHashes.InputY, Player.Instance.m_PlayerMoveInput.y, 0.1f, Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.C)) Player.Instance.m_IsCrouching = !Player.Instance.m_IsCrouching;
            
            Player.Instance.m_PlayerJumpInput = Input.GetKeyDown(KeyCode.Space);
            
            Player.Instance.m_PlayerReloadInput = Input.GetKeyDown(KeyCode.R);

            Player.Instance.m_PlayerShootInput = Input.GetMouseButtonDown(0);
            
            m_PlayerStateManager.SpeedUp(Input.GetKey(KeyCode.LeftShift));
            
        }
    }
}