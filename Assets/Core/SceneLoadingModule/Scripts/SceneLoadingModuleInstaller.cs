using Core.DIContainer.Scripts;
using Global.Scripts.Generated;

namespace Core.SceneLoadingModule.Scripts
{
    public class SceneLoadingModuleInstaller : IInstaller<SceneLoadingModuleInstaller>
    {
        public void Install(DIContainer.Scripts.DIContainer container)
        {
            var sceneLoadingService = container
                .Bind<SceneLoadingServiceFacade>()
                .AsType<ISceneLoadingService>()
                .Register();
            
            sceneLoadingService.SetAddressablesSceneNames(Address.Scenes.AllKeys);
        }
    }
}