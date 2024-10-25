using Animation_Event_Scripts;
using Cinemachine;
using Sirenix.OdinInspector;
using SOs;
using UnityEngine;

namespace Player_Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [field: SerializeField] public PlayerDataSo PlayerData { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Animator RigAnimator { get; private set; }
        [field: SerializeField] public WeaponAnimationEvents WeaponAnimationEvents { get; private set; }
        [field: SerializeField] public GameObject BodyPod { get; private set; }
        [field: SerializeField] public GameObject HandPod { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera CinemachineVcCamera { get; private set; }
        
        [ReadOnly] public Vector2 m_PlayerMoveInput;
        [ReadOnly] public Vector3 m_Velocity;
        [ReadOnly] public Vector3 m_RootMotion;
        
        [ReadOnly] public bool m_PlayerJumpInput;
        [ReadOnly] public bool m_PlayerReloadInput;
        [ReadOnly] public bool m_PlayerShootInput;
        [ReadOnly] public bool m_PlayerCrouchAimInput;
        [ReadOnly] public bool m_PlayerLeanRightAimInput;
        [ReadOnly] public bool m_PlayerLeanLeftAimInput;
        [ReadOnly] public bool m_IsJumping;
        [ReadOnly] public bool m_IsFalling;
        [ReadOnly] public bool m_IsReloading;
        [ReadOnly] public bool m_IsCrouching;
        [ReadOnly] public bool m_SpeedUp;
        [ReadOnly] public bool m_IsCameraZoomIn;

        [ReadOnly] public float m_AnimatorWeight;
        [ReadOnly] public float m_GroundSpeed;
        [ReadOnly] public float m_AirSpeed;
        [ReadOnly] public float m_JumpHeight;
        [ReadOnly] public float m_JumpDamp;
        
        private void Awake() => Instance = this;
    }
}