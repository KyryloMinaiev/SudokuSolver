using Core.DIContainer.Scripts;

namespace Core.ColorSchemeModule.Scripts
{
    public class ColorSchemeModuleInstaller : IInstaller<ColorSchemeModuleInstaller>
    {
        public void Install(DIContainer.Scripts.DIContainer container)
        {
            container.Bind<ColorSchemeConfigurationProvider>().AsType<IColorSchemeConfigurationProvider>().Register();
            container.Bind<ColorSchemeContainer>().AsType<IColorSchemeContainer>().Register();
            container.Bind<ColorSchemePropertyContainer>().AsType<IColorSchemePropertyContainer>().Register();
        }
    }
}