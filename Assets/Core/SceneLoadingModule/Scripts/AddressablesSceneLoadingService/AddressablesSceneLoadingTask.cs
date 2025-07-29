using System;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Core.SceneLoadingModule.Scripts
{
    public class AddressablesSceneLoadingTask : ILoadingTask
    {
        private AsyncOperationHandle _asyncOperation;
        private readonly string _name;
        private LoadingTaskStatus _status;
        private readonly string _sceneName;
        private readonly LoadSceneMode _mode;
        private readonly Action<string, AsyncOperationHandle> _onSceneLoaded;
        
        public AddressablesSceneLoadingTask(string sceneName, bool isAdditive, Action<string, AsyncOperationHandle> onSceneLoaded)
        {
            _mode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            _sceneName = sceneName;
            
            _name = "Loading Scenes...";
            _status = LoadingTaskStatus.Idle;       
            _onSceneLoaded =  onSceneLoaded;
        }

        public string Name => _name;
        public float Progress => Status == LoadingTaskStatus.Idle ? 0 : _asyncOperation.PercentComplete;
        public bool BackgroundTask => false;
        public LoadingTaskStatus Status => _status;

        public void Cancel()
        {
            _asyncOperation.Release();
            _status = LoadingTaskStatus.Cancelled;      
        }

        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;       
            _asyncOperation = Addressables.LoadSceneAsync(_sceneName, _mode);
            await _asyncOperation.Task;
            if (_asyncOperation.Status == AsyncOperationStatus.Succeeded)
            {
                _status =  LoadingTaskStatus.Completed;
                _onSceneLoaded?.Invoke(_sceneName, _asyncOperation);
            }
            else
            {
                _status = LoadingTaskStatus.Failed;
            }
        }
    }
}