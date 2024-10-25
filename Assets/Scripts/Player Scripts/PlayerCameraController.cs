using Cinemachine;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerCameraController : MonoBehaviour
    {
        private Cinemachine3rdPersonFollow _thirdPersonFollow;
        private float _cameraOffset;
        private float _zoomOffset;

        private void Start()
        {
            _thirdPersonFollow = Player.Instance.CinemachineVcCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            _cameraOffset = _thirdPersonFollow.ShoulderOffset.x;
            _zoomOffset = _thirdPersonFollow.ShoulderOffset.z;
        }

        private void Update()
        {
            SetCrouchCameraOffset();
            CameraZoom();
        }

        private void SetCrouchCameraOffset()
        {
            float value = Player.Instance.m_IsCrouching ? Player.Instance.PlayerData.m_CameraOffsetCouchY : Player.Instance.PlayerData.m_CameraOffsetDefaultY;
            
            _cameraOffset = Mathf.Lerp(_cameraOffset, value, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            _thirdPersonFollow.ShoulderOffset.y = _cameraOffset;
        }
        
        private void CameraZoom()
        {
            _zoomOffset = Mathf.Lerp(_zoomOffset, Player.Instance.m_IsCameraZoomIn ? Player.Instance.PlayerData.m_ZoomInValue : Player.Instance.PlayerData.m_ZoomOutValue,
                Time.deltaTime * Player.Instance.PlayerData.m_ZoomInAndOutSpeed);

            _thirdPersonFollow.CameraDistance = _zoomOffset;
        }
    }
}
