using System;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts
{
    [Serializable]
    public struct ColorRoleSettings
    {
        public int ColorRoleID;
        public Color Color;
        
        public ColorRoleSettings(int colorRoleID, Color color)
        {
            ColorRoleID = colorRoleID;
            Color = color;
        }
    }
}