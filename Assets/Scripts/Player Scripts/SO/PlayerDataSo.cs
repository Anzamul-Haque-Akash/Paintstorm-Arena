using Sirenix.OdinInspector;
using UnityEngine;

namespace Player_Scripts.SO
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerDataSo : ScriptableObject
    {
        [Title("Player Move Data")] 
        public float m_Gravity;
        public float m_GroundSpeed;
        public float m_GroundMaxSpeed;
        public float m_JumpHeight;
        public float m_JumpDamp;
        public float m_MaxJumpDamp;
        public float m_StepDown;
        public float m_AirControl;
        public float m_CrouchSpeed;
    }
}
