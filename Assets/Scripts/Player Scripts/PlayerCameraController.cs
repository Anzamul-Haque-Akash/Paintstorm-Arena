using Cinemachine;
using State_Machine;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private PlayerStateManager m_PlayerStateManager;
        
        private Cinemachine3rdPersonFollow _thirdPersonFollow;
        private float _cameraOffset;

        private void Start()
        {
            _thirdPersonFollow = Player.Instance.CinemachineVcCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            _cameraOffset = _thirdPersonFollow.ShoulderOffset.x;
        }

        private void Update()
        {
            SetCrouchCameraOffset();
        }

        private void SetCrouchCameraOffset()
        {
            float value = m_PlayerStateManager.IsCrouching ? Player.Instance.PlayerData.m_CameraOffsetY.y : Player.Instance.PlayerData.m_CameraOffsetY.x;
            
            _cameraOffset = Mathf.Lerp(_cameraOffset, value, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            _thirdPersonFollow.ShoulderOffset.y = _cameraOffset;
        }
    }
}
