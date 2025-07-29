using System;

namespace Content.Features.WindowManagerModule.Scripts
{
    [Serializable]
    public struct WindowRegistryEntry
    {
        public string WindowKey;
        public BaseUIWindow WindowPrefab;
    }
}