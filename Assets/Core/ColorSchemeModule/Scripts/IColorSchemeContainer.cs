using System;

namespace Core.ColorSchemeModule.Scripts
{
    public interface IColorSchemeContainer
    {
        event Action<ColorScheme> OnColorSchemeChanged;
        ColorScheme CurrentColorScheme { get; }
        
        void ApplyColorScheme(ColorScheme colorScheme);
    }
}