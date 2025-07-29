using System.Collections.Generic;
using Core.LoadingServiceModule.Scripts;

namespace Core.SceneLoadingModule.Scripts
{
    public class BuiltInSceneLoadingTaskFactory : ISceneLoadingTaskFactory
    {
        private readonly HashSet<string> _loadedScenes = new HashSet<string>();
        
        public ILoadingTask CreateSceneLoadingTask(string sceneName, bool isAdditive)
        {
            if (!_loadedScenes.Add(sceneName))
            {
                return null;
            }

            return new BuiltInSceneLoadingTask(sceneName, isAdditive);
        }

        public ILoadingTask CreateSceneUnloadingTask(string sceneName)
        {
            if (!_loadedScenes.Contains(sceneName))
            {
                return null;
            }

            _loadedScenes.Remove(sceneName);
            return new BuiltInSceneUnloadingTask(sceneName);
        }
    }
}