using Core.CustomCodeGeneratorModule.Scripts.Editor.Core;
using Core.SourceGeneratorModule.Scripts.Tools;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Content.Features.WindowManagerModule.Scripts.Editor
{
    [Generator]
    public class WindowNamesGenerator : ICodeGenerator
    {
        private const string FILE_NAME = "Windows";
        private const string NAMESPACE_NAME = "Content.Features.WindowManagerModule.Scripts";
        
        public void Execute(GeneratorContext context)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            ClassInstance mainClassInstance = new ClassInstance()
                .AddNamespace(NAMESPACE_NAME)
                .SetPublic()
                .SetStatic()
                .SetName(FILE_NAME);
            
            AddressableAssetGroup windowsGroup = settings.FindGroup("Windows");
            if (windowsGroup == null)
            {
                Debug.LogError("Windows group not found");
                return;
            }

            foreach (var addressableAssetEntry in windowsGroup.entries)
            {
                string assetKey = addressableAssetEntry.address;
                mainClassInstance.AddField(new FieldInstance()
                    .SetPublic()
                    .SetConst()
                    .SetStringType()
                    .SetName(assetKey)
                    .SetAssignedValue(@$"""{assetKey}"""));
            }
            
            context.OverrideFolderPath(GeneratorConstants.ContentFilePath);
            context.AddFile(FILE_NAME + GeneratorConstants.GeneratedFileEnding, mainClassInstance.GetString());
        }
    }
}