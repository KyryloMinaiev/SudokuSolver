using Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel.GridCardUI;
using Content.Features.UIModule.Scripts;
using Core.SudokuLibraryModule.Scripts;
using UnityEngine.Events;

namespace Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel
{
    public class SudokuGridDataPanelViewModel
    {
        private readonly ISudokuLibraryContainer _sudokuLibraryContainer;
        private readonly ISudokuGridUserIntentEmitter _sudokuGridUserIntentEmitter;

        private SudokuLibrary _sudokuLibrary;

        public readonly ReactivePropertyList<GridDataCardInfo> SudokuGridDataList = new();
        public UnityAction AddNewSudokuGridDataCommand { get; }
        public UnityAction<long> DeleteSudokuGridDataCommand { get; }
        public UnityAction<long> EditSudokuGridDataCommand { get; }

        public SudokuGridDataPanelViewModel(ISudokuLibraryContainer sudokuLibraryContainer,
            ISudokuGridUserIntentEmitter sudokuGridUserIntentEmitter)
        {
            _sudokuLibraryContainer = sudokuLibraryContainer;
            _sudokuGridUserIntentEmitter = sudokuGridUserIntentEmitter;

            _sudokuLibraryContainer.OnSudokuLibraryChanged += OnSudokuLibraryChanged;
            OnSudokuLibraryChanged(_sudokuLibraryContainer.CurrentSudokuLibrary);

            DeleteSudokuGridDataCommand = DeleteSudokuGridData;
            EditSudokuGridDataCommand = EditSudokuGridData;
            AddNewSudokuGridDataCommand = AddNewSudokuGridData;
        }

        private void OnSudokuLibraryChanged(SudokuLibrary sudokuLibrary)
        {
            OnRemoveListeners();
            _sudokuLibrary = sudokuLibrary;
            UpdateSudokuGridDataList();
        }

        private void UpdateSudokuGridDataList()
        {
            SudokuGridDataList.Clear();
            if (_sudokuLibrary == null)
            {
                return;
            }
            
            foreach (var sudokuGridData in _sudokuLibrary.SudokuGrids)
            {
                SudokuGridDataList.Add(new GridDataCardInfo(sudokuGridData.GridID, sudokuGridData.Size,
                    sudokuGridData.ChallengesCount, DeleteSudokuGridData, EditSudokuGridData));
            }
        }

        private void OnRemoveListeners()
        {
            if (_sudokuLibrary != null)
            {
                _sudokuLibrary.OnGridAdded -= OnSudokuGridDataAdded;
                _sudokuLibrary.OnGridRemoved -= OnSudokuGridDataRemoved;
            }
        }

        private void OnSudokuGridDataRemoved(SudokuGridData gridData)
        {
            for (int i = SudokuGridDataList.Count - 1; i >= 0; i--)
            {
                if (gridData.GridID == SudokuGridDataList[i].GridID)
                {
                    SudokuGridDataList.RemoveAt(i);
                    break;
                }
            }
        }

        private void OnSudokuGridDataAdded(SudokuGridData gridData)
        {
            SudokuGridDataList.Add(new GridDataCardInfo(gridData.GridID, gridData.Size,
                gridData.ChallengesCount, DeleteSudokuGridData, EditSudokuGridData));
        }

        private void EditSudokuGridData(long gridID)
        {
            _sudokuGridUserIntentEmitter.RequestEdit(gridID);
        }

        private void AddNewSudokuGridData()
        {
            _sudokuGridUserIntentEmitter.RequestCreation();
        }

        private void DeleteSudokuGridData(long gridID)
        {
            _sudokuLibrary.RemoveGridData(gridID);
        }
    }
}