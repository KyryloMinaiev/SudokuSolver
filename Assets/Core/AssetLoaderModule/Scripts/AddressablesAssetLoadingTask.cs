using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.AssetLoaderModule.Scripts
{
    public class AddressablesAssetLoadingTask<T> : ILoadingTask<T> where T : UnityEngine.Object
    {
        private string _path;
        private string _name;
        private AsyncOperationHandle<T> _asyncOperation;
        private LoadingTaskStatus _status = LoadingTaskStatus.Idle;
        private T _result;

        public string Name => _name;
        public float Progress => _asyncOperation.PercentComplete;
        public bool BackgroundTask => false;
        public LoadingTaskStatus Status => _status;
        public T Result => _result;

        public AddressablesAssetLoadingTask()
        {
            _name = "Loading Assets...";
            _status = LoadingTaskStatus.Idle;       
        }
        
        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;       
            _asyncOperation = Addressables.LoadAssetAsync<T>(_path);
            await _asyncOperation.Task;
            _result = _asyncOperation.Result;
            _status = _asyncOperation.Status == AsyncOperationStatus.Succeeded ? LoadingTaskStatus.Completed : LoadingTaskStatus.Failed;
        }

        public void Cancel()
        {
            _asyncOperation.Release();
            _status = LoadingTaskStatus.Cancelled;
        }

        public void Setup(string path)
        {
            _path = path;
        }

        public void Clean()
        {
            _asyncOperation.Release();
            _path = null;
            _result = null;
            _status = LoadingTaskStatus.Idle;      
        }
    }
}