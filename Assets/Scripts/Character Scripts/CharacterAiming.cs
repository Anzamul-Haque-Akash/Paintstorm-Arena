using UnityEngine;

namespace Character_Scripts
{
    public class CharacterAiming : MonoBehaviour
    {
        [SerializeField] private float m_TurnSpeed;
        
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            float yawCamera = _mainCamera.transform.eulerAngles.y;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f), m_TurnSpeed * Time.fixedDeltaTime);
        }
    }
}
