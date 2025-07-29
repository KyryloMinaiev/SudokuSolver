using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.MainScreenModule.Scripts.ApplicationControlPanel
{
    public class ApplicationControlPanel : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _loadLibraryButton;
        [SerializeField] private Button _exitButton;
        
        private ApplicationControlPanelViewModel _viewModel;
        
        public void Initialize(ApplicationControlPanelViewModel viewModel)
        {
            _viewModel = viewModel;
            
            _homeButton.onClick.AddListener(_viewModel.HomeButtonCommand);
            _loadLibraryButton.onClick.AddListener(_viewModel.LoadLibraryCommand);
            _exitButton.onClick.AddListener(_viewModel.CloseApplicationCommand);
        }
        
        private void OnDisable()
        {
            _homeButton.onClick.RemoveAllListeners();
            _loadLibraryButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }
    }
}