using Core.ApplicationModuleSystem.Scripts;
using Core.ApplicationStateMachineModule.Scripts;

namespace Content.Features.SolverApplicationStateMachine.Scripts
{
    public class LibraryLoadingApplicationState : BaseApplicationState
    {
        private readonly IApplicationModuleContainer _applicationModuleContainer;
        
        public LibraryLoadingApplicationState(IApplicationModuleContainer applicationModuleContainer, IApplicationStateMachine stateMachine) : base(stateMachine)
        {
            _applicationModuleContainer =  applicationModuleContainer;
        }
        
        public override void Enter()
        {
            _applicationModuleContainer.ActivateModule<LibraryLoadingModule.Scripts.LoadingLibraryModule>();
        }

        public override void Exit()
        {
            _applicationModuleContainer.DeactivateModule<LibraryLoadingModule.Scripts.LoadingLibraryModule>();
        }
    }
}