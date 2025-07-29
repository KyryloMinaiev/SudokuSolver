using System;
using System.Collections.Generic;

namespace Core.SceneLoadingModule.Scripts
{
    public interface ISceneLoadingService
    {
        void LoadSceneAsMain(string sceneName, Action onSceneLoaded = null);
        void LoadSceneAsAdditive(string sceneName, Action onSceneLoaded = null);
        void LoadScenesAsAdditive(List<string> sceneNames, Action onScenesLoaded = null);
        
        void UnloadScene(string sceneName, Action onSceneUnloaded = null);
    }
}