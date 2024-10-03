namespace State_Machine
{
    public abstract class PlayerBaseState
    {
        public abstract void EnterState(PlayerStateManager playerStateManager);
        public abstract void UpdateState();
        
        public abstract void FixedUpdateState();
    }
}