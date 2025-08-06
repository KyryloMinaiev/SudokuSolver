using System;
using Content.Features.UIModule.Scripts;

namespace Core.ColorSchemeModule.Scripts
{
    public class ColorSchemePropertyContainer : IColorSchemePropertyContainer, IDisposable
    {
        private readonly IColorSchemeContainer _colorSchemeContainer;
        
        public ReactiveProperty<ColorScheme> ColorScheme { get; }

        public ColorSchemePropertyContainer(IColorSchemeContainer colorSchemeContainer)
        {
            ColorScheme = new ReactiveProperty<ColorScheme>();
            _colorSchemeContainer = colorSchemeContainer;
            _colorSchemeContainer.OnColorSchemeChanged += OnColorSchemeChanged;
            ColorScheme.Value = _colorSchemeContainer.CurrentColorScheme;
        }

        private void OnColorSchemeChanged(ColorScheme colorScheme)
        {
            ColorScheme.Value = colorScheme;
        }
        
        public void Dispose()
        {
            _colorSchemeContainer.OnColorSchemeChanged -= OnColorSchemeChanged;
        }
    }
}