using Core.DIContainer.Scripts;

namespace Core.AssetLoaderModule.Scripts
{
    public class AssetLoaderModuleInstaller : IInstaller<AssetLoaderModuleInstaller>
    {
        public void Install(DIContainer.Scripts.DIContainer container)
        {
            container
                .Bind<AddressablesAssetLoader>()
                .AsType<IAssetLoader>()
                .Register();
        }
    }
}