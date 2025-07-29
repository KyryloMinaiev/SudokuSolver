using Core.DIContainer.Scripts;

namespace Content.Features.SolverApplicationClosingModule.Scripts
{
    public class ApplicationClosingModuleInstaller : IInstaller<ApplicationClosingModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container.Bind<ApplicationClosingService>().AsType<IApplicationClosingService>().Register();
        }
    }
}