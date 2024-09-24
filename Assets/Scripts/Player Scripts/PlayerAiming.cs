using UnityEngine;
using UnityEngine.Animations.Rigging;
using Weapon_Scripts;

namespace Player_Scripts
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private float m_TurnSpeed;
        [SerializeField] private Rig m_AimLayerRig;
        [SerializeField] private float m_AimDuration;

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
            if (Input.GetMouseButton(1)) m_AimLayerRig.weight += Time.deltaTime / m_AimDuration;
            else m_AimLayerRig.weight -= Time.deltaTime / m_AimDuration;

            _isAiming = m_AimLayerRig.weight >= 1; 

            if (_isAiming) if (Input.GetMouseButtonDown(0)) _raycastWeapon.StartFiring();
            if (Input.GetMouseButtonUp(0)) _raycastWeapon.StopFiring();
        }
    }
}