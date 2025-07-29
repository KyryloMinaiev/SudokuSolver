using System;

namespace Core.LoadingServiceModule.Scripts
{
    public interface ILoadingService
    {
        event Action<ILoadingTask> OnTaskStarted;
        event Action<float> OnCurrentTaskProgressChanged;
        event Action OnCurrentTaskCompleted;

        void EnqueueLoadingTask(ILoadingTask task, Action onTaskExecuted = null);
    }
}