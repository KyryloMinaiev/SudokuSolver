using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel.GridCardUI
{
    [RequireComponent(typeof(Image))]
    public class HoverInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Action _onPointerEnter;
        private Action _onPointerExit;
        
        public void Initialize(Action onPointerEnter, Action onPointerExit)
        {
            _onPointerEnter = onPointerEnter;
            _onPointerExit = onPointerExit;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onPointerExit?.Invoke();
        }
    }
}
