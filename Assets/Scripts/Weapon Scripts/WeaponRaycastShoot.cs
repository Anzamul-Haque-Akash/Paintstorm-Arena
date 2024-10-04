using UnityEngine;

namespace Weapon_Scripts
{
    public class WeaponRaycastShoot : MonoBehaviour
    {
        [SerializeField] private Transform m_RayCastOrigin;
        [SerializeField] private Transform m_RaycastDestination;
        [SerializeField] private GameObject m_Projectile;
        [SerializeField] private Transform m_ProjectileOrigin;
        [SerializeField] private WeaponRecoil m_WeaponRecoil;
        
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _direction;

        public void Shoot()
        {
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
    }
}