using Core.ApplicationStateMachineModule.Scripts;
using Core.DIContainer.Scripts;

namespace Content.Features.SolverApplicationStateMachine.Scripts
{
    public class StateMachineModuleInstaller : IInstaller<StateMachineModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<SolverApplicationStateMachine>()
                .AsType<IApplicationStateMachine>()
                .Register();
            
            container
                .Bind<BootApplicationState>()
                .AsType<BootApplicationState>()
                .Register();
            
            container
                .Bind<MainScreenApplicationState>()
                .AsType<MainScreenApplicationState>()
                .Register();
            
            container
                .Bind<LibraryLoadingApplicationState>()
                .AsType<LibraryLoadingApplicationState>()
                .Register();
        }
    }
}