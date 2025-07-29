using Core.ApplicationModuleSystem.Scripts;
using Core.ApplicationStateMachineModule.Scripts;

namespace Content.Features.SolverApplicationStateMachine.Scripts
{
    public class BootApplicationState : BaseApplicationState
    {
        private readonly IApplicationModuleContainer _applicationModuleContainer;

        public BootApplicationState(IApplicationModuleContainer applicationModuleContainer, IApplicationStateMachine stateMachine) : base(stateMachine)
        {
            _applicationModuleContainer = applicationModuleContainer;
        }
        
        public override void Enter()
        {
            _applicationModuleContainer.ActivateModule<BootModule.Scripts.BootModule>();
        }
        
        public override void Exit()
        {
            _applicationModuleContainer.DeactivateModule<BootModule.Scripts.BootModule>();
        }
    }
}