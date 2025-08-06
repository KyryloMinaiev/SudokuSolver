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
        
        public void ShowSudokuGridDataListPanel()
        {
            ShowPanel(_sudokuGridDataListPanel);
        }

        public void ShowSudokuGridDataCreationPanel()
        {
            ShowPanel(_sudokuGridDataCreationPanel);
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