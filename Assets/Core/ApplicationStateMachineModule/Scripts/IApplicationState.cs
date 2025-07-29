namespace Core.ApplicationStateMachineModule.Scripts
{
    public interface IApplicationState
    {
        void Enter();
        void Exit();
    }
}