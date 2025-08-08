using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts
{
    [Serializable]
    public class ColorScheme
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private List<ColorRoleSettings> _colorRoleSettings;

        private Dictionary<int, Color> _colorRoleDictionary;
        
        public string Name => _name;
        public List<ColorRoleSettings> ColorRoleSettings => _colorRoleSettings;
        
        public ColorScheme(string name, List<ColorRoleSettings> colorRoleSettings)
        {
            _name = name;
            _colorRoleSettings = colorRoleSettings;
        }

        public void Initialize()
        {
            _colorRoleDictionary = new Dictionary<int, Color>();
            foreach (ColorRoleSettings colorRoleSettings in _colorRoleSettings)
            {
                _colorRoleDictionary[colorRoleSettings.ColorRoleID] = colorRoleSettings.Color;
            }
        }

        public Color GetColor(int colorRoleID)
        {
            return _colorRoleDictionary.GetValueOrDefault(colorRoleID);
        }
    }
}