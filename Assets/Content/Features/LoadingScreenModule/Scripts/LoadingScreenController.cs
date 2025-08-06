using System;
using Content.Features.ScreensModule.Scripts;
using Core.DIContainer.Scripts;
using Core.LoadingServiceModule.Scripts;
using Global.Scripts.Generated;

namespace Content.Features.LoadingScreenModule.Scripts
{
    public class LoadingScreenController : IDisposable, IInitializable
    {
        private readonly IScreenManager _screenManager;
        private readonly ILoadingService _loadingService;
        private readonly LoadingScreenViewModel _loadingScreenViewModel;

        public LoadingScreenController(IScreenManager screenManager, ILoadingService loadingService)
        {
            _screenManager = screenManager;
            _loadingService = loadingService;
            _loadingScreenViewModel = new LoadingScreenViewModel();
        }
        
        public void Initialize()
        {
            _screenManager.PrepareScreen<LoadingScreen>(Address.UIScreens.LoadingScreen);
            _loadingService.OnCurrentTaskCompleted += OnCurrentTaskCompleted;
            _loadingService.OnTaskStarted += OnTaskStarted;
            _loadingService.OnCurrentTaskProgressChanged += OnCurrentTaskProgressChanged;
        }
        
        public void Dispose()
        {
            _loadingService.OnCurrentTaskCompleted -= OnCurrentTaskCompleted;
            _loadingService.OnTaskStarted -= OnTaskStarted;
            _loadingService.OnCurrentTaskProgressChanged -= OnCurrentTaskProgressChanged;
        }

        private void OnCurrentTaskProgressChanged(float progress)
        {
            _loadingScreenViewModel.TaskProgress.Value = progress;
        }

        private void OnTaskStarted(ILoadingTask task)
        {
            if (!task.BackgroundTask)
            {
                _screenManager.ShowScreen<LoadingScreen>();
                _loadingScreenViewModel.TaskDescription.Value = task.Name;
            }
        }

        private void OnCurrentTaskCompleted()
        {
            _screenManager.HideScreen<LoadingScreen>();
        }
    }
}