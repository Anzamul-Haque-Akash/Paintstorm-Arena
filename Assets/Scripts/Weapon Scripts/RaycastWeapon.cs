using UnityEngine;

namespace Weapon_Scripts
{
    public class RaycastWeapon : MonoBehaviour
    {
        [SerializeField] private Transform m_RayCastOrigin;
        [SerializeField] private Transform m_RatcastDestination;
        [SerializeField] private GameObject m_Projectile;
        [field:SerializeField] public bool IsFiring { get; private set;}

        private Ray _ray;
        private RaycastHit _hitInfo;
        private Vector3 _direction;

        private void Start() => IsFiring = false;
        
        public void StartFiring()
        {
            IsFiring = true;

            _direction = (m_RatcastDestination.position - m_RayCastOrigin.position).normalized;

            _ray.origin = m_RayCastOrigin.position;
            _ray.direction = _direction;

            Quaternion targetRotation = Quaternion.LookRotation(_direction);

            Instantiate(m_Projectile, _ray.origin, targetRotation);

            if (Physics.Raycast(_ray, out _hitInfo))
            {
                Debug.DrawLine(_ray.origin, _hitInfo.point, Color.red, 1f);
            }
        }

        public void StopFiring() => IsFiring = false;
    }
}