using Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Weapon_Scripts;

namespace Player_Scripts
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private float m_TurnSpeed;

        [SerializeField] private MultiAimConstraint m_Spine1MultiAimConstraint;
        [SerializeField] private MultiPositionConstraint m_WeaponMultiPositionConstraint;
        [SerializeField] private CinemachineCameraOffset m_CameraOffset;

        private Camera _mainCamera;
        private RaycastWeapon _raycastWeapon;
        private bool _isAiming;

        private void Start()
        {
            _mainCamera = Camera.main;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _raycastWeapon = GetComponentInChildren<RaycastWeapon>();
        }

        private void FixedUpdate()
        {
            float yawCamera = _mainCamera.transform.eulerAngles.y;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f),m_TurnSpeed * Time.fixedDeltaTime);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) _raycastWeapon.StartFiring();

            if (Input.GetKey(KeyCode.E)) Band(-50, 0.15f, 0.42f);
            if (Input.GetKey(KeyCode.Q)) Band(0f, 0.08f, 0.25f);
        }

        private void Band(float spineOffsetZ, float weaponPosOffsetX, float cameraOffsetX)
        {
            Vector3 newOffset;
            
            MultiAimConstraintData aimConstraintData = m_Spine1MultiAimConstraint.data;
            newOffset = aimConstraintData.offset;
            newOffset.z = spineOffsetZ;
            aimConstraintData.offset = newOffset;
            m_Spine1MultiAimConstraint.data = aimConstraintData;

            MultiPositionConstraintData posConstraintData = m_WeaponMultiPositionConstraint.data;
            newOffset = posConstraintData.offset;
            newOffset.x = weaponPosOffsetX;
            posConstraintData.offset = newOffset;
            m_WeaponMultiPositionConstraint.data = posConstraintData;

            m_CameraOffset.m_Offset.x = cameraOffsetX;
        }
    }
}