using System.Threading;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneLoadingModule.Scripts
{
    public class BuiltInSceneUnloadingTask : ILoadingTask
    {
        private readonly string _sceneName;
        private readonly string _name;
        private AsyncOperation _asyncOperation;
        private LoadingTaskStatus _status;
        
        public BuiltInSceneUnloadingTask(string sceneName)
        {
            _sceneName = sceneName;
            _name = "Loading...";
            _asyncOperation = null;
            _status = LoadingTaskStatus.Idle;
        }

        public string Name => _name;
        public float Progress => _asyncOperation?.progress ?? 0;
        public bool BackgroundTask => false;
        public LoadingTaskStatus Status => _status;

        public  async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;
            _asyncOperation = SceneManager.UnloadSceneAsync(_sceneName);
            await _asyncOperation.ToUniTask();
            _status = LoadingTaskStatus.Completed;
        }

        public void Cancel()
        {
            _asyncOperation.WithCancellation(CancellationToken.None, true);
            _status = LoadingTaskStatus.Cancelled;
        }
    }
}