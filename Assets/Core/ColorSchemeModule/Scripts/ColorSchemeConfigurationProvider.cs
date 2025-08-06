using System.Collections.Generic;
using Core.AssetLoaderModule.Scripts;
using Core.DIContainer.Scripts;

namespace Core.ColorSchemeModule.Scripts
{
    public class ColorSchemeConfigurationProvider : IColorSchemeConfigurationProvider, IInitializable
    {
        private readonly IAssetLoader _assetLoader;
        private List<ColorScheme> _colorSchemes;
        
        public ColorSchemeConfigurationProvider(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public async void Initialize()
        {
            var configuration = await _assetLoader.LoadAssetAsync<ColorSchemeConfiguration>("ColorSchemeConfiguration");
            _colorSchemes = configuration.ColorSchemes;
        }
        
        public List<ColorScheme> GetColorSchemes()
        {
            return _colorSchemes;
        }
    }
}