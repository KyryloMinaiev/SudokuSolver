using UnityEngine;

namespace Content.Features.ScreensModule.Scripts
{
    public class MainCanvasContainer : MonoBehaviour
    {
        [SerializeField] private Canvas _mainCanvas;
        private Transform _mainCanvasTransform;

        public Transform MainCanvasTransform => _mainCanvasTransform;
        public Canvas MainCanvas => _mainCanvas;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _mainCanvasTransform = _mainCanvas.transform;
        }
    }
}
