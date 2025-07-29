using System;
using UnityEngine;

namespace Content.Features.ScreensModule.Scripts
{
    public abstract class BaseUIScreen : MonoBehaviour
    {
        private bool _opened;
        
        public event Action<BaseUIScreen> OnScreenClosed;
        public bool IsOpened => _opened;

        public void OpenScreen()
        {
            gameObject.SetActive(true);
            _opened = true;
        }
        
        public void CloseScreen()
        {
            _opened = false;
            gameObject.SetActive(false);
            OnScreenClosed?.Invoke(this);
        }
    }
}