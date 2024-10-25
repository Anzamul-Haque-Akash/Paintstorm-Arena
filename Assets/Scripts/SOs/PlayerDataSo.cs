using Sirenix.OdinInspector;
using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerDataSo : ScriptableObject
    {
        [Title("Player Move Data"), BoxGroup] 
        public float m_GroundSpeed;
        public float m_GroundMaxSpeed;
        public float m_CrouchSpeed;
        [Title("Player Jump And Air Data"), BoxGroup] 
        public float m_JumpHeight;
        public float m_JumpMaxHeight;
        public float m_JumpDamp;
        public float m_MaxJumpDamp;
        public float m_AirSpeed;
        public float m_AirMaxSpeed;
        public float m_Gravity;
        public float m_StepDown;
        
        [Header("Player Lean Aim Data"), BoxGroup]
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
   
        [Header("Player Crouch Aim Data"), BoxGroup]
        public float m_CameraOffsetDefaultY;
        public float m_CameraOffsetCouchY;
        [Space]
        public float m_CrouchAimOffsetYDefault;
        public float m_CrouchAimOffsetY;
        
        [Header("Player Camera Zoom IN and OUT Data"), BoxGroup]
        public float m_ZoomInValue;
        public float m_ZoomOutValue;
        
        [Space] [Title("Player Aiming Data"), BoxGroup] 
        public float m_TurnSpeed;
        public float m_LeanAimSpeed;
        public float m_CrouchAimSpeed;
        public float m_ZoomInAndOutSpeed;
        
    }
}