using UnityEngine;

namespace State_Machine
{
    public class PlayerIdleState : PlayerBaseState
    {
        public override void EnterState(PlayerStateManager playerStateManager)
        {
            Debug.Log("Player in idle state.");
        }

        public override void UpdateState()
        {
            Debug.Log("Player idle state updating.");
        }

        public override void FixedUpdateState()
        {
            
        }
    }
}
