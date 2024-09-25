using UnityEngine;

namespace Weapon_Scripts
{
    public class WeaponRecoil : MonoBehaviour
    {
        public Cinemachine.CinemachineFreeLook m_PlayerCamera;
        public Cinemachine.CinemachineImpulseSource m_CameraShake;
        public Vector2[] m_RecoilPattern;
        public float m_Duration;
        public Animator m_RigLayerAnimator;
        
        private float _verticalRecoil;
        private float _horizontalRecoil;
        private int _index;
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

            _index = Random.Range(0, m_RecoilPattern.Length);
            _horizontalRecoil = m_RecoilPattern[_index].x;
            
            _index = Random.Range(0, m_RecoilPattern.Length);
            _verticalRecoil = m_RecoilPattern[_index].y;
            
            m_RigLayerAnimator.SetTrigger("isShooting");
            
        }
        
        private void Update()
        {
            if (_time > 0)
            {
                m_PlayerCamera.m_YAxis.Value -= (_verticalRecoil * Time.deltaTime) / m_Duration;
                m_PlayerCamera.m_XAxis.Value -= (_horizontalRecoil * Time.deltaTime) / m_Duration;
                _time -= Time.deltaTime;
            }
        }
    }
}
