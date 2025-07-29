using System.Collections.Generic;
using Core.LoadingServiceModule.Scripts;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.SceneLoadingModule.Scripts
{
    public class AddressablesSceneLoadingTaskFactory : ISceneLoadingTaskFactory
    {
        private readonly Dictionary<string, AsyncOperationHandle> _sceneLoadingHandles = new ();

        public ILoadingTask CreateSceneLoadingTask(string sceneName, bool isAdditive)
        {
            if (_sceneLoadingHandles.ContainsKey(sceneName))
            {
                return default;
            }
            
            AddressablesSceneLoadingTask task = new AddressablesSceneLoadingTask(sceneName, isAdditive, OnSceneLoaded);
            
            return task;
        }

        private void OnSceneLoaded(string sceneName, AsyncOperationHandle handle)
        {
            _sceneLoadingHandles[sceneName] = handle;
        }

        public ILoadingTask CreateSceneUnloadingTask(string sceneName)
        {
            if (_sceneLoadingHandles.Remove(sceneName, out var sceneLoadingHandle))
            {
                return new AddressablesSceneUnloadingTask(sceneLoadingHandle, sceneName);
            }

            return default;
        }
    }
}