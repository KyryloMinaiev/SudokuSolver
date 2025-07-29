using System;
using System.Collections.Generic;
using Core.DIContainer.Scripts;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;

namespace Content.Features.SolverApplication.Scripts
{
    public class SolverApplicationLoadingService : ILoadingService, IUpdatable
    {
        private struct LoadingTaskData
        {
            public readonly ILoadingTask Task;
            public readonly Action OnTaskExecuted;
            
            public LoadingTaskData(ILoadingTask task, Action onTaskExecuted)
            {
                Task = task;
                OnTaskExecuted = onTaskExecuted;
            }
        }
        
        private readonly Queue<LoadingTaskData> _loadingTasksQueue = new();
        private ILoadingTask _currentTask;
        private float _currentTaskProgress;

        public event Action<ILoadingTask> OnTaskStarted;
        public event Action<float> OnCurrentTaskProgressChanged;
        public event Action OnCurrentTaskCompleted;

        public async UniTask ExecuteLoadingTask(ILoadingTask task, Action onTaskExecuted = null)
        {
            _currentTask = task;
            OnTaskStarted?.Invoke(task);
            await task.Execute();
            onTaskExecuted?.Invoke();
            OnCurrentTaskCompleted?.Invoke();
            _currentTask = null;
        }
        
        public void EnqueueLoadingTask(ILoadingTask task, Action onTaskExecuted = null)
        {
            if (task == null)
            {
                return;
            }
            
            _loadingTasksQueue.Enqueue(new LoadingTaskData(task, onTaskExecuted));
        }

        public async void Update()
        {
            if (_currentTask is { Status: LoadingTaskStatus.Running })
            {
                if (_currentTask.Progress != _currentTaskProgress)
                {
                    UpdateTaskProgress(_currentTask.Progress);
                }
            }
            else
            {
                await ExecuteNextTask();
            }
        }

        private async UniTask ExecuteNextTask()
        {
            if (_loadingTasksQueue.Count == 0)
            {
                return;
            }
            
            LoadingTaskData loadingTaskData = _loadingTasksQueue.Dequeue();
            await ExecuteLoadingTask(loadingTaskData.Task, loadingTaskData.OnTaskExecuted);
        }

        private void UpdateTaskProgress(float progress)
        {
            _currentTaskProgress = progress;
            OnCurrentTaskProgressChanged?.Invoke(_currentTaskProgress);
        }
    }
}