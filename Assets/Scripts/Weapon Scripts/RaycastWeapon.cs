using UnityEngine;

namespace Weapon_Scripts
{
    public class RaycastWeapon : MonoBehaviour
    {
        public Transform m_RayCastOrigin;
        public Transform m_RatcastDestination;
        public GameObject m_Projectile;
        public float m_ProjectileForce;
        public bool m_IsFiring;

        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _direction;
        
        private void Start()
        {
            m_IsFiring = false;
        }

        public void StartFiring()
        {
            m_IsFiring = true;

            _direction = m_RatcastDestination.position - m_RayCastOrigin.position;

            _ray.origin = m_RayCastOrigin.position;
            _ray.direction = _direction;
            
            GameObject projectile = Instantiate(m_Projectile, _ray.origin, Quaternion.identity);
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            
            if(Physics.Raycast(_ray, out _hitInfo))
            {
                projectileRb.AddForce(_direction * m_ProjectileForce, ForceMode.Impulse);
                Debug.DrawLine(_ray.origin, _hitInfo.point, Color.red, 1f);
            }
        }

        public void StopFiring()
        {
            m_IsFiring = false;
        }
    }
}
