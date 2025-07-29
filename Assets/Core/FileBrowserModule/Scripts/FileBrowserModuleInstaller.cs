using Core.DIContainer.Scripts;

namespace Core.FileBrowserModule.Scripts
{
    public class FileBrowserModuleInstaller : IInstaller<FileBrowserModuleInstaller>
    {
        public void Install(DIContainer.Scripts.DIContainer container)
        {
            container
                .Bind<StandaloneFileBrowserService>()
                .AsType<IFileBrowserService>()
                .Register();
        }
    }
}