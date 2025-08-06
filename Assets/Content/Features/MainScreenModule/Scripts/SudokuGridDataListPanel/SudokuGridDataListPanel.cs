using System.Collections.Generic;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel.GridCardUI;
using Core.DIContainer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel
{
    public class SudokuGridDataListPanel : MainScreenBasePanel
    {
        [SerializeField] private Button _createNewSudokuGridDataButton;
        [SerializeField] private Transform _gridDataCardsContainer;
        [SerializeField] private GridDataCard _gridDataCardPrefab;

        private readonly List<GridDataCard> _createdGridDataCards = new List<GridDataCard>();
        private SudokuGridDataPanelViewModel _viewModel;

        [Inject]
        public void Initialize(SudokuGridDataPanelViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void OnEnable()
        {
            _viewModel.SudokuGridDataList.Subscribe(UpdateGridDataCards);
            UpdateGridDataCards(_viewModel.SudokuGridDataList.Value);
            _createNewSudokuGridDataButton.onClick.AddListener(_viewModel.AddNewSudokuGridDataCommand);
        }

        private void UpdateGridDataCards(List<GridDataCardInfo> gridDataCardInfoList)
        {
            while (gridDataCardInfoList.Count > _createdGridDataCards.Count)
            {
                var card = Instantiate(_gridDataCardPrefab, _gridDataCardsContainer);
                _createdGridDataCards.Add(card);
            }

            for (int i = 0; i < _createdGridDataCards.Count; i++)
            {
                var card = _createdGridDataCards[i];
                
                if (i < gridDataCardInfoList.Count)
                {
                    card.gameObject.SetActive(true);
                    card.Initialize(gridDataCardInfoList[i]);
                }
                else
                {
                    card.gameObject.SetActive(false);
                }
            }
        }

        private void OnDisable()
        {
            _createNewSudokuGridDataButton.onClick.RemoveListener(_viewModel.AddNewSudokuGridDataCommand);
        }
    }
}