using Sirenix.OdinInspector;
using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerDataSo : ScriptableObject
    {
        [Title("Player Move Data")] public float m_Gravity;
        public float m_GroundSpeed;
        public float m_GroundMaxSpeed;
        public float m_JumpHeight;
        public float m_JumpMaxHeight;
        public float m_JumpDamp;
        public float m_MaxJumpDamp;
        public float m_AirSpeed;
        public float m_AirMaxSpeed;
        public float m_CrouchSpeed;
        public float m_StepDown;

        [Space] [Title("Player Aiming Data")] public float m_TurnSpeed;
        [Header("Player Lean Aim Data")] public Vector3 m_SpineOffsetZ;
        public Vector3 m_HeadOffsetZ;
        public Vector3 m_WeaponPosOffsetX;
        public Vector3 m_CameraOffsetX;
        public float m_LeanSpeed;
    }
}