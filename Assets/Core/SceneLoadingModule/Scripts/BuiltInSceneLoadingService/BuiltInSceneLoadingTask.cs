using System.Threading;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneLoadingModule.Scripts
{
    public class BuiltInSceneLoadingTask : ILoadingTask
    {
        private AsyncOperation _asyncOperation;
        private readonly string _sceneName;
        private readonly bool _isAdditive;
        private readonly string _name;
        private LoadingTaskStatus _status;

        public BuiltInSceneLoadingTask(string sceneName, bool isAdditive)
        {
            _sceneName = sceneName;
            _isAdditive = isAdditive;
            _name = "Loading Scenes...";
            _asyncOperation = null;
            _status = LoadingTaskStatus.Idle;       
        }

        public string Name => _name;
        public float Progress => _asyncOperation?.progress ?? 0;
        public bool BackgroundTask => false;
        public LoadingTaskStatus Status => _status;

        public void Cancel()
        {
            _asyncOperation.WithCancellation(CancellationToken.None, true);
            _status = LoadingTaskStatus.Cancelled;       
        }

        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;       
            LoadSceneMode loadSceneMode = _isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            _asyncOperation = SceneManager.LoadSceneAsync(_sceneName, loadSceneMode);
            await _asyncOperation.ToUniTask();
            _status = _asyncOperation.isDone ? LoadingTaskStatus.Completed : LoadingTaskStatus.Failed;
        }
    }
}