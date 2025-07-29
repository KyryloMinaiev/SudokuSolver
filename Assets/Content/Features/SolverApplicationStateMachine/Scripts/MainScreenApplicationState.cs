using Core.ApplicationModuleSystem.Scripts;
using Core.ApplicationStateMachineModule.Scripts;

namespace Content.Features.SolverApplicationStateMachine.Scripts
{
    public class MainScreenApplicationState : BaseApplicationState
    {
        private readonly IApplicationModuleContainer _applicationModuleContainer;
        
        public MainScreenApplicationState(IApplicationModuleContainer applicationModuleContainer, IApplicationStateMachine stateMachine) : base(stateMachine)
        {
            _applicationModuleContainer = applicationModuleContainer;
        }
        
        public override void Enter()
        {
            _applicationModuleContainer.ActivateModule<MainScreenModule.Scripts.MainScreenModule>();
        }

        public override void Exit()
        {
            _applicationModuleContainer.DeactivateModule<MainScreenModule.Scripts.MainScreenModule>();
        }
    }
}