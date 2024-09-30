using UnityEngine;
using UnityEngine.Animations.Rigging;
using Weapon_Scripts;

namespace Player_Scripts
{
    public class AimingController : MonoBehaviour
    {
        [SerializeField] private float m_TurnSpeed;

        [SerializeField] private MultiAimConstraint m_Spine1MultiAimConstraint;
        [SerializeField] private MultiAimConstraint m_HeadMultiAimConstraint;
        [SerializeField] private MultiPositionConstraint m_WeaponMultiPositionConstraint;
        [SerializeField] private CinemachineCameraOffset m_CinemachineCameraOffset;
        
        [SerializeField] private Vector3 m_SpineOffsetZ;
        [SerializeField] private Vector3 m_HeadOffsetZ;
        [SerializeField] private Vector3 m_WeaponPosOffsetX;
        [SerializeField] private Vector3 m_CameraOffsetX;
        
        [SerializeField] private float m_SmoothSpeed;

        private Camera _mainCamera;
        private RaycastWeapon _raycastWeapon;
        private bool _isAiming;
        
        private float _spineOffset;
        private float _headOffset;
        private float _weaponOffset;
        private float _cameraOffset;

        private void Start()
        {
            _mainCamera = Camera.main;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _raycastWeapon = GetComponentInChildren<RaycastWeapon>();
            
            _spineOffset = m_Spine1MultiAimConstraint.data.offset.z;
            _headOffset = m_HeadMultiAimConstraint.data.offset.z;
            _weaponOffset = m_WeaponMultiPositionConstraint.data.offset.x;
            _cameraOffset = m_CinemachineCameraOffset.m_Offset.x;
        }

        private void FixedUpdate()
        {
            float yawCamera = _mainCamera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f),m_TurnSpeed * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) _raycastWeapon.StartFiring();

            if (Input.GetKey(KeyCode.E)) Band(m_SpineOffsetZ.z, m_HeadOffsetZ.z, m_WeaponPosOffsetX.z, m_CameraOffsetX.z);
            else if (Input.GetKey(KeyCode.Q)) Band(m_SpineOffsetZ.x, m_HeadOffsetZ.x, m_WeaponPosOffsetX.x, m_CameraOffsetX.x);
            else Band(m_SpineOffsetZ.y, m_HeadOffsetZ.y, m_WeaponPosOffsetX.y, m_CameraOffsetX.y);
        }
        
        private void Band(float spineOffsetZ, float headOffsetZ, float weaponPosOffsetX, float cameraOffsetX)
        {
            Vector3 newOffset;
            
            MultiAimConstraintData constraintData = m_Spine1MultiAimConstraint.data;
            _spineOffset = Mathf.Lerp(_spineOffset, spineOffsetZ, Time.deltaTime * m_SmoothSpeed);
            newOffset = constraintData.offset;
            newOffset.z = _spineOffset;
            constraintData.offset = newOffset;
            m_Spine1MultiAimConstraint.data = constraintData;
            
            constraintData = m_HeadMultiAimConstraint.data;
            _headOffset = Mathf.Lerp(_headOffset, headOffsetZ, Time.deltaTime * m_SmoothSpeed);
            newOffset = constraintData.offset;
            newOffset.z = _headOffset;
            constraintData.offset = newOffset;
            m_HeadMultiAimConstraint.data = constraintData;
            
            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            _weaponOffset = Mathf.Lerp(_weaponOffset, weaponPosOffsetX, Time.deltaTime * m_SmoothSpeed);
            newOffset = posConstraintData.offset;
            newOffset.x = _weaponOffset;
            posConstraintData.offset = newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;
            
            _cameraOffset = Mathf.Lerp(_cameraOffset, cameraOffsetX, Time.deltaTime * m_SmoothSpeed);
            m_CinemachineCameraOffset.m_Offset.x = _cameraOffset;
        }
    }
}