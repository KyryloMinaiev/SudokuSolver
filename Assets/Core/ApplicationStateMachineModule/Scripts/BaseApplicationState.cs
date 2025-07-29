namespace Core.ApplicationStateMachineModule.Scripts
{
    public abstract class BaseApplicationState : IApplicationState
    {
        protected BaseApplicationState(IApplicationStateMachine applicationStateMachine)
        {
            applicationStateMachine.AddState(this);
        }
        
        public abstract void Enter();
        public abstract void Exit();
    }
}