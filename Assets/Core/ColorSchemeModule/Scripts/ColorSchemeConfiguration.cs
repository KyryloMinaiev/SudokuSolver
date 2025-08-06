using System.Collections.Generic;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts
{
    [CreateAssetMenu(fileName = "ColorSchemeConfiguration", menuName = "Sudoku/ColorSchemeConfiguration", order = 0)]
    public class ColorSchemeConfiguration : ScriptableObject
    {
        [SerializeField] private List<ColorRole> _colorRoles;
        [SerializeField] private List<ColorScheme> _colorSchemes;
        
        public List<ColorRole> ColorRoles => _colorRoles;
        public List<ColorScheme> ColorSchemes => _colorSchemes;

        private void OnEnable()
        {
            foreach (ColorScheme colorScheme in _colorSchemes)
            {
                colorScheme.Initialize();
            }
        }
    }
}