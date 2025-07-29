using Core.DIContainer.Scripts;

namespace Content.Features.WindowManagerModule.Scripts
{
    public class WindowManagerModuleInstaller : IInstaller<WindowManagerModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<WindowManager>()
                .AsType<IWindowManager>()
                .Register();
        }
    }
}