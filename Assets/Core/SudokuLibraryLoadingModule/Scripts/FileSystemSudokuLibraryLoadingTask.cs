using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public class FileSystemSudokuLibraryLoadingTask : ILoadingTask<string>
    {
        private const string NAME = "Loading Sudoku Library...";
        private readonly string _path;
        private string _result;
        private Task<string> _task;
        private CancellationTokenSource _cancellationTokenSource;
        private LoadingTaskStatus _status;
        
        public FileSystemSudokuLibraryLoadingTask(string path)
        {
            _path = path;
            _result = null;
            _task = null;
            _cancellationTokenSource = null;
            _status = LoadingTaskStatus.Idle;
        }

        public string Name => NAME;
        public float Progress => _task.IsCompleted ? 1 : 0;
        public bool BackgroundTask => false;
        public LoadingTaskStatus Status => _status;

        public async UniTask Execute()
        {
            _status = LoadingTaskStatus.Running;
            if (!File.Exists(_path))
            {
                _status = LoadingTaskStatus.Failed;
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _task = File.ReadAllTextAsync(_path, _cancellationTokenSource.Token);
            _result = await _task;
            _status = _task.IsCompletedSuccessfully ? LoadingTaskStatus.Completed : LoadingTaskStatus.Failed;
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            _status = LoadingTaskStatus.Cancelled;
        }

        public string Result => _result;
    }
}