using System.Collections.Generic;
using Core.AssetLoaderModule.Scripts;
using Core.DIContainer.Scripts;
using Global.Scripts.Generated;

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

        public void Initialize()
        {
            var configuration = _assetLoader.LoadAsset<ColorSchemeConfiguration>(Address.ScriptableObjects.ColorSchemeConfiguration);
            _colorSchemes = configuration.ColorSchemes;
        }
        
        public List<ColorScheme> GetColorSchemes()
        {
            return _colorSchemes;
        }

        public ColorScheme GetColorScheme(string name)
        {
            return _colorSchemes.Find(x => x.Name == name);
        }
    }
}