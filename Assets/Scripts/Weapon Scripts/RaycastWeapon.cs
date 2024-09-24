using UnityEngine;

namespace Weapon_Scripts
{
    public class RaycastWeapon : MonoBehaviour
    {
        public Transform m_RayCastOrigin;
        public bool m_IsFiring;

        private Ray _ray;
        private RaycastHit _hitInfo;
        
        private void Start()
        {
            m_IsFiring = false;
        }

        public void StartFiring()
        {
            m_IsFiring = true;

            _ray.origin = m_RayCastOrigin.position;
            _ray.direction = m_RayCastOrigin.forward;
            
            if(Physics.Raycast(_ray, out _hitInfo))
            {
                Debug.DrawLine(_ray.origin, _hitInfo.point, Color.red, 1f);
            }
        }

        public void StopFiring()
        {
            m_IsFiring = false;
        }
    }
}
