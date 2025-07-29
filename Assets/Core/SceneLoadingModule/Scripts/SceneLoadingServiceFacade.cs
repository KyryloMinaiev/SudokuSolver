using System;
using System.Collections.Generic;
using Core.LoadingServiceModule.Scripts;

namespace Core.SceneLoadingModule.Scripts
{
    public class SceneLoadingServiceFacade : ISceneLoadingService
    {
        private HashSet<string> _addressablesSceneNames;
        private readonly ISceneLoadingTaskFactory _builtInSceneLoadingService;
        private readonly ISceneLoadingTaskFactory _addressablesSceneLoadingService;
        private readonly ILoadingService _loadingService;

        public SceneLoadingServiceFacade(ILoadingService loadingService)
        {
            _loadingService = loadingService;
            _builtInSceneLoadingService = new BuiltInSceneLoadingTaskFactory();
            _addressablesSceneLoadingService = new AddressablesSceneLoadingTaskFactory();
        }

        public void SetAddressablesSceneNames(HashSet<string> addressablesSceneNames)
        {
            _addressablesSceneNames = addressablesSceneNames;
        }

        public void LoadSceneAsMain(string sceneName, Action onSceneLoaded = null)
        {
            LoadScene(sceneName, false, onSceneLoaded);
        }

        public void LoadSceneAsAdditive(string sceneName, Action onSceneLoaded = null)
        {
            LoadScene(sceneName, true, onSceneLoaded);
        }

        private void LoadScene(string sceneName, bool isAdditive, Action onSceneLoaded)
        {
            ILoadingTask task = GetSceneLoadingTask(sceneName, isAdditive);
            _loadingService.EnqueueLoadingTask(task, onSceneLoaded);
        }

        private ILoadingTask GetSceneLoadingTask(string sceneName, bool isAdditive)
        {
            if (_addressablesSceneNames.Contains(sceneName))
            {
                return _addressablesSceneLoadingService.CreateSceneLoadingTask(sceneName, isAdditive);
            }

            return _builtInSceneLoadingService.CreateSceneLoadingTask(sceneName, isAdditive);
        }
        
        private ILoadingTask GetSceneUnloadingTask(string sceneName)
        {
            if (_addressablesSceneNames.Contains(sceneName))
            {
                return _addressablesSceneLoadingService.CreateSceneUnloadingTask(sceneName);
            }

            return _builtInSceneLoadingService.CreateSceneUnloadingTask(sceneName);
        }

        public void LoadScenesAsAdditive(List<string> sceneNames, Action onScenesLoaded = null)
        {
            var tasks = new List<ILoadingTask>();
            foreach (var sceneName in sceneNames)
            {
                ILoadingTask task = GetSceneLoadingTask(sceneName, true);;
                if (task != null)
                {
                    tasks.Add(task);
                }
            }

            _loadingService.EnqueueLoadingTask(new LoadingTaskList(tasks), onScenesLoaded);
        }

        public void UnloadScene(string sceneName, Action onSceneUnloaded = null)
        {
            ILoadingTask task = GetSceneUnloadingTask(sceneName);
            _loadingService.EnqueueLoadingTask(task, onSceneUnloaded);
        }
    }
}