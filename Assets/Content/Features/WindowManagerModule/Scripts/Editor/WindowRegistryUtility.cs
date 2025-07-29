using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace Content.Features.WindowManagerModule.Scripts.Editor
{
    public static class WindowRegistryUtility
    {
        public static void UpdateWindowRegistry(WindowRegistry windowRegistry)
        {
            var setting = AddressableAssetSettingsDefaultObject.Settings;
            
            var windowGroup = setting.FindGroup("Windows");
            if (windowGroup == null)
            {
                Debug.LogError("Window group not found");
                return;
            }
            
            for (int i = 0; i < windowRegistry.WindowRegistryEntries.Count; i++)
            {
                var entry = windowRegistry.WindowRegistryEntries[i];
                if (entry.WindowPrefab == null)
                {
                    continue;
                }

                string windowKey = entry.WindowPrefab.name;
                entry.WindowKey = windowKey;
                string assetPath = AssetDatabase.GetAssetPath(entry.WindowPrefab);
                string assetGUID = AssetDatabase.AssetPathToGUID(assetPath);

                var addressableEntry = setting.CreateOrMoveEntry(assetGUID, windowGroup, false);
                addressableEntry.SetAddress(windowKey);
                
                windowRegistry.WindowRegistryEntries[i] = entry;
            }
        }

        public static void GenerateWindowsKeysClass(WindowRegistry windowRegistry)
        {
            
        }
    }
}