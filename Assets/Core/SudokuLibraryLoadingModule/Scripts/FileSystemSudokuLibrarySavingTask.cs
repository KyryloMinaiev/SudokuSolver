using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public class FileSystemSudokuLibrarySavingTask : ILoadingTask
    {
        private const string NAME = "Saving Sudoku Library...";
        private readonly string _path;
        private readonly string _content;
        private Task _task;
        private CancellationTokenSource _cancellationTokenSource;
        private LoadingTaskStatus _status;
        
        public FileSystemSudokuLibrarySavingTask(string path, string content)
        {
            _path = path;
            _content = content;
            _task = null;
            _cancellationTokenSource = null;
            _status = LoadingTaskStatus.Idle;
        }

        public string Name => NAME;
        public float Progress => _task.IsCompleted ? 1 : 0;
        public bool BackgroundTask => true;
        public LoadingTaskStatus Status => _status;
        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            _status = LoadingTaskStatus.Cancelled;
        }

        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;
            _cancellationTokenSource = new CancellationTokenSource();
            _task = File.WriteAllTextAsync(_path, _content, _cancellationTokenSource.Token);
            await _task;
            _status = _task.IsCompletedSuccessfully ? LoadingTaskStatus.Completed : LoadingTaskStatus.Failed;
        }
    }
}