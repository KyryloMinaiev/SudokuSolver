using System.Collections.Generic;

namespace Core.ApplicationStateMachineModule.Scripts
{
    public interface IApplicationStateMachine
    {
        void EnterState<TState>() where TState : IApplicationState;
        void SetStates<TState>(List<TState> states) where TState : IApplicationState;
        void AddState<TState>(TState state) where TState : IApplicationState;
    }
}