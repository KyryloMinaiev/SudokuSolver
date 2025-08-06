using System.Collections.Generic;

namespace Core.ColorSchemeModule.Scripts
{
    public interface IColorSchemeConfigurationProvider
    {
        List<ColorScheme> GetColorSchemes();
    }
}