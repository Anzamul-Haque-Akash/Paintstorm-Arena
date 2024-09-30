using UnityEngine;

namespace Weapon_Scripts
{
    public class RaycastWeapon : MonoBehaviour
    {
        [SerializeField] private Transform m_RayCastOrigin;
        [SerializeField] private Transform m_RaycastDestination;
        [SerializeField] private GameObject m_Projectile;
        [SerializeField] private Transform m_ProjectileOrigin;
        [SerializeField] private WeaponRecoil m_WeaponRecoil;

        [field: SerializeField] public bool IsFiring { get; private set; }

        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _direction;

        private void Start() => IsFiring = false;

        public void StartFiring()
        {
            IsFiring = true;

            _direction = (m_RaycastDestination.position - m_RayCastOrigin.position).normalized;

            _ray.origin = m_RayCastOrigin.position;
            _ray.direction = _direction;

            Quaternion targetRotation = Quaternion.LookRotation(_direction);

            Instantiate(m_Projectile, m_ProjectileOrigin.position, targetRotation);

            if (Physics.Raycast(_ray, out _hitInfo))
            {
                Debug.DrawLine(_ray.origin, _hitInfo.point, Color.red, 1f);
            }

            m_WeaponRecoil.GenerateRecoil();
        }

        public void StopFiring() => IsFiring = false;
    }
}