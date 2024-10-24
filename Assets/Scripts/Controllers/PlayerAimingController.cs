using Cinemachine;
using Player_Scripts;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Weapon_Scripts;

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
        private WeaponRaycastShoot _weaponRaycastShoot;
        private bool _isZoomIn;

        private float _spineOffset;
        private float _headOffset;
        private float _weaponOffsetX;
        private float _weaponOffsetY;
        private Vector3 _newOffset;
        private float _cameraOffset;
        private float _zoomOffset;
        private Cinemachine3rdPersonFollow _thirdPersonFollow;

        private void Start()
        {
            _mainCamera = Camera.main;
            _thirdPersonFollow = Player.Instance.CinemachineVcCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _weaponRaycastShoot = GetComponentInChildren<WeaponRaycastShoot>();

            _spineOffset = m_Spine1MultiAimConstraint.data.offset.z;
            _headOffset = m_HeadMultiAimConstraint.data.offset.z;
            _weaponOffsetX = m_WeaponMultiPositionConstraint.data.offset.x;
            _weaponOffsetY = m_WeaponMultiPositionConstraint.data.offset.y;
            _cameraOffset = _thirdPersonFollow.ShoulderOffset.x;
            _zoomOffset = _thirdPersonFollow.ShoulderOffset.z;
        }

        private void FixedUpdate()
        {
            m_Xaxis.Update(Time.fixedDeltaTime);
            m_Yaxis.Update(Time.fixedDeltaTime);
            m_CameraLookAt.eulerAngles = new Vector3(m_Yaxis.Value, m_Xaxis.Value, 0f);
            
            float yawCamera = _mainCamera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f),
                Player.Instance.PlayerData.m_TurnSpeed * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Player.Instance.m_IsReloading) _weaponRaycastShoot.Shoot();
            
            CrouchAim(Input.GetKey(KeyCode.X) ? Player.Instance.PlayerData.m_CrouchAimOffsetY : Player.Instance.PlayerData.m_CrouchAimOffsetYDefault);
            
            if (Input.GetKey(KeyCode.E))
                LeanAim(Player.Instance.PlayerData.m_SpineOffsetZ.z, Player.Instance.PlayerData.m_HeadOffsetZ.z,
                    Player.Instance.PlayerData.m_WeaponPosOffsetX.z, Player.Instance.PlayerData.m_CameraOffsetX.z);
            
            else if (Input.GetKey(KeyCode.Q))
                LeanAim(Player.Instance.PlayerData.m_SpineOffsetZ.x, Player.Instance.PlayerData.m_HeadOffsetZ.x,
                    Player.Instance.PlayerData.m_WeaponPosOffsetX.x, Player.Instance.PlayerData.m_CameraOffsetX.x);
            else
                LeanAim(Player.Instance.PlayerData.m_SpineOffsetZ.y, Player.Instance.PlayerData.m_HeadOffsetZ.y, 
                    Player.Instance.PlayerData.m_WeaponPosOffsetX.y, Player.Instance.PlayerData.m_CameraOffsetX.y);

            _isZoomIn = Input.GetMouseButton(1);
            CameraZoom();
        }

        private void LeanAim(float spineOffsetZ, float headOffsetZ, float weaponPosOffsetX, float cameraOffsetX)
        {
            MultiAimConstraintData constraintData = m_Spine1MultiAimConstraint.data;
            _spineOffset = Mathf.Lerp(_spineOffset, spineOffsetZ, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            _newOffset = constraintData.offset;
            _newOffset.z = _spineOffset;
            constraintData.offset = _newOffset;
            m_Spine1MultiAimConstraint.data = constraintData;

            constraintData = m_HeadMultiAimConstraint.data;
            _headOffset = Mathf.Lerp(_headOffset, headOffsetZ, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            _newOffset = constraintData.offset;
            _newOffset.z = _headOffset;
            constraintData.offset = _newOffset;
            m_HeadMultiAimConstraint.data = constraintData;

            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            _weaponOffsetX = Mathf.Lerp(_weaponOffsetX, weaponPosOffsetX, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            _newOffset = posConstraintData.offset;
            _newOffset.x = _weaponOffsetX;
            posConstraintData.offset = _newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;

            _cameraOffset = Mathf.Lerp(_cameraOffset, cameraOffsetX, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            _thirdPersonFollow.ShoulderOffset.x = _cameraOffset;
        }

        private void CrouchAim(float weaponPosOffsetX)
        {
            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            _weaponOffsetY = Mathf.Lerp(_weaponOffsetY, weaponPosOffsetX, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            _newOffset = posConstraintData.offset;
            _newOffset.y = _weaponOffsetY;
            posConstraintData.offset = _newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;
            
        }

        private void CameraZoom()
        {
            _zoomOffset = Mathf.Lerp(_zoomOffset, _isZoomIn 
                ? Player.Instance.PlayerData.m_ZoomInValue 
                : Player.Instance.PlayerData.m_ZoomOutValue, 
                Time.deltaTime * Player.Instance.PlayerData.m_ZoomInAndOutSpeed);
            
            _thirdPersonFollow.CameraDistance = _zoomOffset;
        }
    }
}