using Core.DIContainer.Scripts;

namespace Core.ColorSchemeModule.Scripts
{
    public class ColorSchemeInitialization : IInitializable
    {
        private readonly IColorSchemeLoadingService _colorSchemeLoadingService;
        
        public ColorSchemeInitialization(IColorSchemeLoadingService colorSchemeLoadingService)
        {
            _colorSchemeLoadingService = colorSchemeLoadingService;
        }
        
        public void Initialize()
        {
            _colorSchemeLoadingService.LoadSavedColorScheme();
        }
    }
}