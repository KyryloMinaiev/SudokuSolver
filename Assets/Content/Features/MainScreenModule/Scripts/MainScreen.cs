using Content.Features.MainScreenModule.Scripts.ApplicationControlPanel;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataCreationPanel;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel;
using Content.Features.ScreensModule.Scripts;
using UnityEngine;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreen : BaseUIScreen
    {
        [SerializeField] private ApplicationControlPanel.ApplicationControlPanel _controlPanel;
        [SerializeField] private SudokuGridDataListPanel.SudokuGridDataListPanel _sudokuGridDataListPanel;
        [SerializeField] private SudokuGridDataCreationPanel.SudokuGridDataCreationPanel _sudokuGridDataCreationPanel;

        private MainScreenBasePanel _currentPanel;
        
        public void Initialize(ApplicationControlPanelViewModel applicationControlPanelViewModel)
        {
            _controlPanel.Initialize(applicationControlPanelViewModel);
        }

        public void ShowSudokuGridDataListPanel(SudokuGridDataPanelViewModel viewModel)
        {
            ShowPanel(_sudokuGridDataListPanel);
            _sudokuGridDataListPanel.Initialize(viewModel);
        }

        public void ShowSudokuGridDataCreationPanel(SudokuGridDataCreationPanelViewModel viewModel)
        {
            ShowPanel(_sudokuGridDataCreationPanel);
            _sudokuGridDataCreationPanel.Initialize(viewModel);
        }

        private void ShowPanel(MainScreenBasePanel panel)
        {
            HideCurrentPanel();
            panel.ShowPanel();
            _currentPanel = panel;
        }

        private void HideCurrentPanel()
        {
            if (_currentPanel != null)
            {
                _currentPanel.HidePanel();
            }
        }
    }
}