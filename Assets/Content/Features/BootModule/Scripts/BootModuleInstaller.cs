using System.Collections.Generic;
using Content.Features.SolverApplicationStateMachine.Scripts;
using Content.Features.WindowManagerModule.Scripts;
using Core.ApplicationModuleSystem.Scripts;
using Core.ApplicationStateMachineModule.Scripts;
using Core.DIContainer.Scripts;
using Core.SceneLoadingModule.Scripts;

namespace Content.Features.BootModule.Scripts
{
    public class BootModuleInstaller : IInstaller<BootModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            var moduleContainer = container.Get<IApplicationModuleContainer>();
            var stateMachine = container.Get<IApplicationStateMachine>();

            moduleContainer.BindModule(new BootModule(container.Get<ISceneLoadingService>(), stateMachine,
                new List<IPreloadingService>
                {
                    new WindowsPreloadingService(container.Get<IWindowManager>())
                }));
        }
    }
}