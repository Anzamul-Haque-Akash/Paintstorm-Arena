using Sirenix.OdinInspector;
using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerDataSo : ScriptableObject
    {
        [Title("Player Move Data")] 
        public float m_GroundSpeed;
        public float m_GroundMaxSpeed;
        public float m_CrouchSpeed;
        
        [Title("Player Jump And Air Data")] 
        public float m_JumpHeight;
        public float m_JumpMaxHeight;
        public float m_JumpDamp;
        public float m_MaxJumpDamp;
        public float m_AirSpeed;
        public float m_AirMaxSpeed;
        public float m_Gravity;
        public float m_StepDown;
        
        [Header("Player Lean Aim Data")]
        public float m_SpineOffsetDefaultZ;
        public float m_SpineOffsetLeftZ;
        public float m_SpineOffsetRightZ;
        [Space]
        public float m_HeadOffsetDefaultZ;
        public float m_HeadOffsetLeftZ;
        public float m_HeadOffsetRightZ;
        [Space]
        public float m_WeaponPosOffsetDefaultX;
        public float m_WeaponPosOffsetLeftX;
        public float m_WeaponPosOffsetRightX;
        [Space]
        public float m_CameraOffsetDefaultX;
        public float m_CameraOffsetLeftX;
        public float m_CameraOffsetRightX;
   
        [Header("Player Hand Up Aim Data")]
        public float m_CameraOffsetDefaultY;
        public float m_CameraOffsetY;
        [Space]
        public float m_CameraOffsetCrouchDefaultY;
        public float m_CameraOffsetCrouchY;
        [Space]
        public float m_HandUpAimOffsetDefaultY;
        public float m_HandUpAimOffsetY;
        
        [Header("Player Camera Zoom IN and OUT Data")]
        public float m_ZoomInValue;
        public float m_ZoomOutValue;
        
        [Space] [Title("Player Aiming Data")] 
        public float m_TurnSpeed;
        public float m_AimSpeed;
        public float m_ZoomInAndOutSpeed;
    }
}