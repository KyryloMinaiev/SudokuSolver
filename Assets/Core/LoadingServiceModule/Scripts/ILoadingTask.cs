using Cysharp.Threading.Tasks;

namespace Core.LoadingServiceModule.Scripts
{
    public interface ILoadingTask
    {
        string Name { get; }
        float Progress { get; }
        bool BackgroundTask { get; }
        LoadingTaskStatus Status { get; }
        
        void Cancel();
        UniTask Execute();
    }
    
    public interface ILoadingTask<out T> : ILoadingTask
    {
        T Result { get; }
    }
}