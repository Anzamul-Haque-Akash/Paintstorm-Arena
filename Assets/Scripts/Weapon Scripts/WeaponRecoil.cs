using UnityEngine;

namespace Weapon_Scripts
{
    public class WeaponRecoil : MonoBehaviour
    {
        public Cinemachine.CinemachineFreeLook m_PlayerCamera;
        public float m_VerticalRecoil;
        public float m_Duration;

        private float _time;

        public void GenerateRecoil()
        {
            _time = m_Duration;
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
