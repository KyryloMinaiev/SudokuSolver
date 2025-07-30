using Content.Features.ScreensModule.Scripts;
using Core.DIContainer.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.LoadingScreenModule.Scripts
{
    public class LoadingScreen : BaseUIScreen
    {
        [SerializeField] private TMP_Text _taskProgress;
        [SerializeField] private TMP_Text _taskDescription;
        [SerializeField] private Slider _taskProgressSlider;
        
        private LoadingScreenViewModel _viewModel;
        
        [Inject]
        public void Initialize(LoadingScreenViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            _viewModel.TaskProgress.Subscribe(UpdateTaskProgress);
            _viewModel.TaskDescription.Subscribe(UpdateTaskDescription);
        }

        private void UpdateTaskDescription(string value)
        {
            _taskDescription.text = value;
        }
        
        private void UpdateTaskProgress(float value)
        {
            _taskProgress.text = $"{Mathf.RoundToInt(value * 100)}%";
            _taskProgressSlider.value = value;
        }

        private void OnDestroy()
        {
            _viewModel.TaskProgress.Unsubscribe(UpdateTaskProgress);
            _viewModel.TaskDescription.Unsubscribe(UpdateTaskDescription);
        }
    }
}