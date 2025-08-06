using Core.DIContainer.Scripts;

namespace Content.Features.LoadingScreenModule.Scripts
{
    public class LoadingScreenModuleInstaller : IInstaller<LoadingScreenModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<LoadingScreenViewModel>()
                .AsType<LoadingScreenViewModel>()
                .Register();
            
            container
                .Bind<LoadingScreenController>()
                .AsType<LoadingScreenController>()
                .Register();
        }
    }
}