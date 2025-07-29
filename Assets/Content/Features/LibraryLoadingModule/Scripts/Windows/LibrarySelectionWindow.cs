using Content.Features.WindowManagerModule.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.LibraryLoadingModule.Scripts.Windows
{
    public class LibrarySelectionWindow : BaseUIWindow
    {
        private const string EmptyPath = "Enter library path...";
        
        [SerializeField] private TMP_Text _libraryPathLabel;
        [SerializeField] private Button _openFileDialogButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _createLibraryButton;
        [SerializeField] private Button _openLibraryButton;
        
        private LibrarySelectionViewModel _librarySelectionViewModel;
        
        public void Initialize(LibrarySelectionViewModel librarySelectionViewModel,
                bool showCancelButton)
        {
            _librarySelectionViewModel = librarySelectionViewModel;
            
            _openFileDialogButton.onClick.AddListener(_librarySelectionViewModel.ChoosePathCommand);
            _cancelButton.onClick.AddListener(_librarySelectionViewModel.CancelCommand);
            _createLibraryButton.onClick.AddListener(_librarySelectionViewModel.CreateNewLibraryCommand);
            _openLibraryButton.onClick.AddListener(_librarySelectionViewModel.OpenLibraryCommand);
            _cancelButton.gameObject.SetActive(showCancelButton);

            SubscribeToViewModel();
        }

        private void SubscribeToViewModel()
        {
            _librarySelectionViewModel.SelectedPath.Subscribe(UpdateLibraryPathText);
            _librarySelectionViewModel.ShowCancelButton.Subscribe(UpdateCancelButtonVisibility);
        }

        private void UpdateCancelButtonVisibility(bool showCancelButton)
        {
            _cancelButton.gameObject.SetActive(showCancelButton);
        }

        private void UpdateLibraryPathText(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                _libraryPathLabel.text = EmptyPath;
            }
            else
            {
                _libraryPathLabel.text = path;
            }
        }

        private void OnDisable()
        {
            _openFileDialogButton.onClick.RemoveListener(_librarySelectionViewModel.ChoosePathCommand);
            _cancelButton.onClick.RemoveListener(_librarySelectionViewModel.CancelCommand);
            _createLibraryButton.onClick.RemoveListener(_librarySelectionViewModel.CreateNewLibraryCommand);
            _openLibraryButton.onClick.RemoveListener(_librarySelectionViewModel.OpenLibraryCommand);
            _librarySelectionViewModel.SelectedPath.Unsubscribe(UpdateLibraryPathText);
        }
    }
}