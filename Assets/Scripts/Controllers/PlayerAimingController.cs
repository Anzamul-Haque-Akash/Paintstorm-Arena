using Cinemachine;
using Player_Scripts;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Weapon_Scripts;

namespace Controllers
{
    public class PlayerAimingController : MonoBehaviour
    {
        public Cinemachine.AxisState m_Xaxis;
        public Cinemachine.AxisState m_Yaxis;
        public Transform m_CmeraLookAt;
        
        [SerializeField] private MultiAimConstraint m_Spine1MultiAimConstraint;
        [SerializeField] private MultiAimConstraint m_HeadMultiAimConstraint;
        [SerializeField] private MultiPositionConstraint m_WeaponMultiPositionConstraint;

        private Camera _mainCamera;
        private WeaponRaycastShoot _weaponRaycastShoot;
        private bool _isAiming;

        private float _spineOffset;
        private float _headOffset;
        private float _weaponOffset;
        private float _cameraOffset;
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
            _weaponOffset = m_WeaponMultiPositionConstraint.data.offset.x;
            _cameraOffset = _thirdPersonFollow.ShoulderOffset.x;
        }

        private void FixedUpdate()
        {
            m_Xaxis.Update(Time.fixedDeltaTime);
            m_Yaxis.Update(Time.fixedDeltaTime);
            m_CmeraLookAt.eulerAngles = new Vector3(m_Yaxis.Value, m_Xaxis.Value, 0f);
            
            float yawCamera = _mainCamera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f),
                Player.Instance.PlayerData.m_TurnSpeed * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Player.Instance.m_IsReloading) _weaponRaycastShoot.Shoot();

            if (Input.GetKey(KeyCode.E))
                Lean(Player.Instance.PlayerData.m_SpineOffsetZ.z, Player.Instance.PlayerData.m_HeadOffsetZ.z,
                    Player.Instance.PlayerData.m_WeaponPosOffsetX.z, Player.Instance.PlayerData.m_CameraOffsetX.z);
            
            else if (Input.GetKey(KeyCode.Q))
                Lean(Player.Instance.PlayerData.m_SpineOffsetZ.x, Player.Instance.PlayerData.m_HeadOffsetZ.x,
                    Player.Instance.PlayerData.m_WeaponPosOffsetX.x, Player.Instance.PlayerData.m_CameraOffsetX.x);
            
            else
                Lean(Player.Instance.PlayerData.m_SpineOffsetZ.y, Player.Instance.PlayerData.m_HeadOffsetZ.y,
                    Player.Instance.PlayerData.m_WeaponPosOffsetX.y, Player.Instance.PlayerData.m_CameraOffsetX.y);
        }

        private void Lean(float spineOffsetZ, float headOffsetZ, float weaponPosOffsetX, float cameraOffsetX)
        {
            Vector3 newOffset;

            MultiAimConstraintData constraintData = m_Spine1MultiAimConstraint.data;
            _spineOffset = Mathf.Lerp(_spineOffset, spineOffsetZ, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            newOffset = constraintData.offset;
            newOffset.z = _spineOffset;
            constraintData.offset = newOffset;
            m_Spine1MultiAimConstraint.data = constraintData;

            constraintData = m_HeadMultiAimConstraint.data;
            _headOffset = Mathf.Lerp(_headOffset, headOffsetZ, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            newOffset = constraintData.offset;
            newOffset.z = _headOffset;
            constraintData.offset = newOffset;
            m_HeadMultiAimConstraint.data = constraintData;

            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            _weaponOffset = Mathf.Lerp(_weaponOffset, weaponPosOffsetX, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            newOffset = posConstraintData.offset;
            newOffset.x = _weaponOffset;
            posConstraintData.offset = newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;

            _cameraOffset = Mathf.Lerp(_cameraOffset, cameraOffsetX, Time.deltaTime * Player.Instance.PlayerData.m_LeanSpeed);
            _thirdPersonFollow.ShoulderOffset.x = _cameraOffset;
        }
    }
}