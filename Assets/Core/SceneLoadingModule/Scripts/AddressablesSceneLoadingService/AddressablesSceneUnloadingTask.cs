using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.SceneLoadingModule.Scripts
{
    public class AddressablesSceneUnloadingTask : ILoadingTask
    {
        private readonly string _sceneName;
        private readonly string _name;
        private readonly AsyncOperationHandle _sceneLoadingHandle;
        private AsyncOperationHandle _asyncOperation;
        private LoadingTaskStatus _status;
        
        public AddressablesSceneUnloadingTask(AsyncOperationHandle sceneLoadingHandle, string sceneName)
        {
            _sceneLoadingHandle = sceneLoadingHandle;
            _sceneName = sceneName;
            _name = "Loading...";
            _asyncOperation = default;
            _status = LoadingTaskStatus.Idle;       
        }
        
        public string Name => _name;

        public float Progress => _asyncOperation.PercentComplete;
        public bool BackgroundTask => false;
        public LoadingTaskStatus Status => _status;

        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;       
            _asyncOperation = Addressables.UnloadSceneAsync(_sceneLoadingHandle);
            await _asyncOperation.Task;
            _status = LoadingTaskStatus.Completed;       
        }

        public void Cancel()
        {
            _asyncOperation.Release();
            _status = LoadingTaskStatus.Cancelled;      
        }
    }
}