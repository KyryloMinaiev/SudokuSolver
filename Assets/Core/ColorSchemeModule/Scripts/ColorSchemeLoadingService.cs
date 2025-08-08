using System;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts
{
    public class ColorSchemeLoadingService : IColorSchemeLoadingService, IDisposable
    {
        private const string ColorSchemeKey = "AppliedColorScheme";
        
        private readonly IColorSchemeConfigurationProvider _colorSchemeConfigurationProvider;
        private readonly IColorSchemeContainer _colorSchemeContainer;

        public ColorSchemeLoadingService(IColorSchemeConfigurationProvider colorSchemeConfigurationProvider,
            IColorSchemeContainer colorSchemeContainer)
        {
            _colorSchemeConfigurationProvider = colorSchemeConfigurationProvider;
            _colorSchemeContainer = colorSchemeContainer;
            
            _colorSchemeContainer.OnColorSchemeChanged += OnColorSchemeChanged;
        }

        private void OnColorSchemeChanged(ColorScheme colorScheme)
        {
            SaveColorScheme(colorScheme.Name);
        }

        private void SaveColorScheme(string colorSchemeName)
        {
            PlayerPrefs.SetString(ColorSchemeKey, colorSchemeName);
        }

        public void LoadSavedColorScheme()
        {
            if (PlayerPrefs.HasKey(ColorSchemeKey))
            {
                ColorScheme colorScheme = _colorSchemeConfigurationProvider.GetColorScheme(PlayerPrefs.GetString(ColorSchemeKey));
                if (colorScheme != null)
                {
                    _colorSchemeContainer.ApplyColorScheme(colorScheme);
                    return;
                }
            }

            var colorSchemes = _colorSchemeConfigurationProvider.GetColorSchemes();
            if (colorSchemes.Count > 0)
            {
                ColorScheme colorScheme = colorSchemes[0];
                _colorSchemeContainer.ApplyColorScheme(colorScheme);
                SaveColorScheme(colorScheme.Name);
            }
        }

        public void Dispose()
        {
            _colorSchemeContainer.OnColorSchemeChanged -= OnColorSchemeChanged;
        }
    }
}