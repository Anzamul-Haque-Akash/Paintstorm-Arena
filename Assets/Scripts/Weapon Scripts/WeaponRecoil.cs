using UnityEngine;

namespace Weapon_Scripts
{
    public class WeaponRecoil : MonoBehaviour
    {
        public Cinemachine.CinemachineFreeLook m_PlayerCamera;
        public Cinemachine.CinemachineImpulseSource m_CameraShake;
        public float m_VerticalRecoil;
        public float m_Duration;

        private float _time;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void GenerateRecoil()
        {
            _time = m_Duration;
            
            m_CameraShake.GenerateImpulse(_camera.transform.forward);
        }

        private void Update()
        {
            if (_time > 0)
            {
                m_PlayerCamera.m_YAxis.Value -= (m_VerticalRecoil * Time.deltaTime) / m_Duration;
                _time -= Time.deltaTime;
            }
        }
    }
}
