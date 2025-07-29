using Core.DIContainer.Scripts;

namespace Content.Features.ScreensModule.Scripts
{
    public class ScreenManagerModuleInstaller : IInstaller<ScreenManagerModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<ScreenManager>()
                .AsType<IScreenManager>()
                .Register();
        }
    }
}