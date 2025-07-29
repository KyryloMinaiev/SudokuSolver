using System.Collections.Generic;
using UnityEngine;

namespace Content.Features.WindowManagerModule.Scripts
{
    [CreateAssetMenu(fileName = "WindowRegistry", menuName = "Registries/WindowRegistry", order = 0)]
    public class WindowRegistry : ScriptableObject
    {
        public List<WindowRegistryEntry> WindowRegistryEntries;
    }
}