using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Core.LoadingServiceModule.Scripts
{
    public class LoadingTaskList : ILoadingTask
    {
        private readonly List<ILoadingTask> _loadingTasks;
        private bool _isExecuting;
        private int _currentTaskIndex;
        private ILoadingTask _currentTask;
        private LoadingTaskStatus _status;
        
        public LoadingTaskList()
        {
            _loadingTasks = new List<ILoadingTask>();
            _status = LoadingTaskStatus.Idle;
        }
        
        public LoadingTaskList(List<ILoadingTask> loadingTasks)
        {
            _loadingTasks = loadingTasks;
            _status = LoadingTaskStatus.Idle;
        }

        public string Name => _currentTask?.Name;
        public float Progress => GetTaskProgress();
        public bool BackgroundTask => _currentTask?.BackgroundTask ?? false;
        public LoadingTaskStatus Status => _status;

        public void AddTask(ILoadingTask task)
        {
            _loadingTasks.Add(task);
        }

        private float GetTaskProgress()
        {
            if (_loadingTasks.Count == 1)
            {
                return _loadingTasks[0].Progress;
            }
            
            return (_currentTaskIndex + 1) / (float) _loadingTasks.Count; 
        }

        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;
            for (int i = 0; i < _loadingTasks.Count; i++)
            {
                if (!_isExecuting)
                {
                    break;
                }
                
                _currentTaskIndex = i;
                _currentTask = _loadingTasks[i];
                await _currentTask.Execute();

                if (_currentTask.Status == LoadingTaskStatus.Failed)
                {
                    _status = LoadingTaskStatus.Failed;
                    break;
                }
            }
            
            _status = LoadingTaskStatus.Completed;
        }

        public void Cancel()
        {
            if (_currentTask != null)
            {
                _currentTask.Cancel();
            }
            
            _isExecuting = false;
            _status = LoadingTaskStatus.Cancelled;
        }
    }
}