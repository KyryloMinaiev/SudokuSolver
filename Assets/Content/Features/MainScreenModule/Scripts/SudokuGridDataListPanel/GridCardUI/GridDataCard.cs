using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel.GridCardUI
{
    public class GridDataCard : MonoBehaviour
    {
        [SerializeField] private HoverInteractable _hoverInteractable;
        [SerializeField] private GameObject _hiddenCardObject;
        [SerializeField] private TMP_Text _sudokuDimensionLabel;
        [SerializeField] private TMP_Text _challengesCountLabel;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private Button _editChallengesButton;

        private GridDataCardInfo _gridDataCardInfo;
        
        private void Start()
        {
            _hoverInteractable.Initialize(OnPointerEnter, OnPointerExit);
            OnPointerExit();
        }

        public void Initialize(GridDataCardInfo gridDataCardInfo)
        {
            _gridDataCardInfo = gridDataCardInfo;
            
            _deleteButton.onClick.AddListener(OnDeleteButtonClick);
            _editChallengesButton.onClick.AddListener(OnEditChallengesButtonClick);
            SetDimensionLabel(_gridDataCardInfo.Dimension);
            SetChallengesCountLabel(_gridDataCardInfo.ChallengesCount);
        }

        private void OnPointerEnter()
        {
            _hiddenCardObject.SetActive(true);
        }
        
        private void OnPointerExit()
        {
            _hiddenCardObject.SetActive(false);
        }
        
        private void SetDimensionLabel(int dimension)
        {
            _sudokuDimensionLabel.text = $"{dimension}x{dimension}";
        }
        
        private void SetChallengesCountLabel(int challengesCount)
        {
            _challengesCountLabel.text = $"Challenges: {challengesCount}";
        }
        
        private void OnDeleteButtonClick()
        {
            _gridDataCardInfo.GridDeleteCommand?.Invoke(_gridDataCardInfo.GridID);
        }
        
        private void OnEditChallengesButtonClick()
        {
            _gridDataCardInfo.GridEditCommand?.Invoke(_gridDataCardInfo.GridID);
        }

        private void OnDisable()
        {
            _deleteButton.onClick.RemoveListener(OnDeleteButtonClick);
            _editChallengesButton.onClick.RemoveListener(OnEditChallengesButtonClick);
        }
    }
}
