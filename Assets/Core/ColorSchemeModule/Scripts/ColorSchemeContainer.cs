using System;

namespace Core.ColorSchemeModule.Scripts
{
    public class ColorSchemeContainer : IColorSchemeContainer
    {
        private ColorScheme _currentColorScheme;
        
        public event Action<ColorScheme> OnColorSchemeChanged;
        public ColorScheme CurrentColorScheme => _currentColorScheme;
        
        public void ApplyColorScheme(ColorScheme colorScheme)
        {
            if (_currentColorScheme != colorScheme)
            {
                _currentColorScheme = colorScheme;
                OnColorSchemeChanged?.Invoke(colorScheme);
            }
        }
    }
}