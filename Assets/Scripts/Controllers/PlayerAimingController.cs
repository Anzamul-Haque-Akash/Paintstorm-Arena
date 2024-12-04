using Cinemachine;
using Player_Scripts;
using SOs;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Controllers
{
    public class PlayerAimingController : MonoBehaviour
    {
        public AxisState m_Xaxis;
        public AxisState m_Yaxis;
        public Transform m_CameraLookAt;

        [SerializeField] private MultiAimConstraint m_Spine1MultiAimConstraint;
        [SerializeField] private MultiAimConstraint m_HeadMultiAimConstraint;
        [SerializeField] private MultiPositionConstraint m_WeaponMultiPositionConstraint;

        private Camera _mainCamera;

        private float _spineOffset;
        private float _headOffset;
        private float _weaponOffsetX;
        private float _weaponOffsetY;
        private Vector3 _newOffset;
        private float _cameraOffsetX;
        private float _cameraOffsetY;
        private Cinemachine3rdPersonFollow _thirdPersonFollowCamera;
        private PlayerDataSo _playerDataSo;

        private void Start()
        {
            _mainCamera = Camera.main;
            _thirdPersonFollowCamera = Player.Instance.CinemachineVcCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            _playerDataSo = Player.Instance.PlayerData;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _spineOffset = m_Spine1MultiAimConstraint.data.offset.z;
            _headOffset = m_HeadMultiAimConstraint.data.offset.z;
            _weaponOffsetX = m_WeaponMultiPositionConstraint.data.offset.x;
            _weaponOffsetY = m_WeaponMultiPositionConstraint.data.offset.y;
            _cameraOffsetX = _thirdPersonFollowCamera.ShoulderOffset.x;
            _cameraOffsetY = _thirdPersonFollowCamera.ShoulderOffset.y;
        }

        private void FixedUpdate()
        {
            m_Xaxis.Update(Time.fixedDeltaTime);
            m_Yaxis.Update(Time.fixedDeltaTime);
            m_CameraLookAt.eulerAngles = new Vector3(m_Yaxis.Value, m_Xaxis.Value, 0f);

            float yawCamera = _mainCamera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f),
                _playerDataSo.m_TurnSpeed * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Player.Instance.m_PlayerHandUpAimInput)
                HandUpAim(_playerDataSo.m_WeaponAimOffsetY,Player.Instance.m_IsCrouching ? _playerDataSo.m_CameraOffsetCrouchY : _playerDataSo.m_CameraOffsetY);
            else
                HandUpAim(_playerDataSo.m_WeaponOffsetDefaultY,Player.Instance.m_IsCrouching ? _playerDataSo.m_CameraOffsetCrouchDefaultY : _playerDataSo.m_CameraOffsetDefaultY);

            
            if (Player.Instance.m_PlayerLeanRightAimInput) 
                LeanAim(_playerDataSo.m_SpineOffsetRightZ, _playerDataSo.m_HeadOffsetRightZ, _playerDataSo.m_WeaponPosOffsetRightX, _playerDataSo.m_CameraOffsetRightX);
            else if (Player.Instance.m_PlayerLeanLeftAimInput)
                LeanAim(_playerDataSo.m_SpineOffsetLeftZ, _playerDataSo.m_HeadOffsetLeftZ, _playerDataSo.m_WeaponPosOffsetLeftX, _playerDataSo.m_CameraOffsetLeftX);
            else
                LeanAim(_playerDataSo.m_SpineOffsetDefaultZ, _playerDataSo.m_HeadOffsetDefaultZ, _playerDataSo.m_WeaponPosOffsetDefaultX, _playerDataSo.m_CameraOffsetDefaultX);
        }

        private void LeanAim(float spineOffsetZ, float headOffsetZ, float weaponPosOffsetX, float cameraOffsetX)
        {
            MultiAimConstraintData constraintData = m_Spine1MultiAimConstraint.data;
            _spineOffset = Mathf.Lerp(_spineOffset, spineOffsetZ, Time.deltaTime * _playerDataSo.m_AimSpeed);
            _newOffset = constraintData.offset;
            _newOffset.z = _spineOffset;
            constraintData.offset = _newOffset;
            m_Spine1MultiAimConstraint.data = constraintData;

            constraintData = m_HeadMultiAimConstraint.data;
            _headOffset = Mathf.Lerp(_headOffset, headOffsetZ, Time.deltaTime * _playerDataSo.m_AimSpeed);
            _newOffset = constraintData.offset;
            _newOffset.z = _headOffset;
            constraintData.offset = _newOffset;
            m_HeadMultiAimConstraint.data = constraintData;

            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            _weaponOffsetX = Mathf.Lerp(_weaponOffsetX, weaponPosOffsetX,Time.deltaTime * _playerDataSo.m_AimSpeed);
            _newOffset = posConstraintData.offset;
            _newOffset.x = _weaponOffsetX;
            posConstraintData.offset = _newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;

            _cameraOffsetX = Mathf.Lerp(_cameraOffsetX, cameraOffsetX, Time.deltaTime * _playerDataSo.m_AimSpeed);
            _thirdPersonFollowCamera.ShoulderOffset.x = _cameraOffsetX;
        }

        private void HandUpAim(float weaponPosOffsetX, float cameraOffsetY)
        {
            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            _weaponOffsetY = Mathf.Lerp(_weaponOffsetY, weaponPosOffsetX, Time.deltaTime * _playerDataSo.m_AimSpeed);
            _newOffset = posConstraintData.offset;
            _newOffset.y = _weaponOffsetY;
            posConstraintData.offset = _newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;
            
            _cameraOffsetY = Mathf.Lerp(_cameraOffsetY, cameraOffsetY, Time.deltaTime * _playerDataSo.m_AimSpeed);
            _thirdPersonFollowCamera.ShoulderOffset.y = _cameraOffsetY;
        }
    }
}