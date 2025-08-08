using System;

namespace Core.ColorSchemeModule.Scripts
{
    [Serializable]
    public struct ColorRole
    {
        public string Name;
        public int ID;
        
        public ColorRole(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}