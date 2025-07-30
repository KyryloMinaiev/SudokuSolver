using System;
using System.Collections.Generic;
using Content.Features.ScreensModule.Scripts;
using Core.AssetLoaderModule.Scripts;
using Core.DIContainer.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Content.Features.WindowManagerModule.Scripts
{
    public class WindowManager : IWindowManager, IInitializable
    {
        private readonly DIContainer _container;
        private readonly IAssetLoader _assetLoader;
        private readonly IScreenManager _screenManager;
        private readonly Dictionary<string, BaseUIWindow> _createdWindows;
        
        private Transform _mainCanvasTransform;

        public WindowManager(IAssetLoader assetLoader, IScreenManager screenManager, DIContainer container)
        {
            _assetLoader = assetLoader;
            _container = container;
            _screenManager = screenManager;
            _createdWindows = new Dictionary<string, BaseUIWindow>();
        }
        
        public async UniTask PrepareWindow<T>(string windowKey, Action<T> onWindowReady = null) where T : BaseUIWindow
        {
            if (_createdWindows.ContainsKey(windowKey))
            {
                return;
            }
            
            GameObject windowPrefab = await _assetLoader.LoadAssetAsync<GameObject>(windowKey);
            T window = _container.InstantiateComponent<T>(windowPrefab, _mainCanvasTransform);
            if (window != null)
            {
                SetupNewWindow(windowKey, window, onWindowReady);
            }
            else
            {
                Object.Destroy(window.gameObject);
            }
        }

        private void SetupNewWindow<T>(string windowKey, T window, Action<T> onWindowReady = null) where T : BaseUIWindow
        {
            _createdWindows[windowKey] = window;
            HideWindow(windowKey);
            onWindowReady?.Invoke(window);
        }

        public TWindow ShowWindow<TWindow>(string windowKey, BaseUIScreen parentScreen) where TWindow : BaseUIWindow
        {
            if (!parentScreen.IsOpened)
            {
                return null;
            }
            
            if (!_createdWindows.TryGetValue(windowKey, out var window))
            {
                return null;
            }
            
            SetupWindow(window, parentScreen.transform, true);
            return (TWindow)window;
        }

        private void SetupWindow(BaseUIWindow window, Transform parent, bool enabled)
        {
            window.gameObject.SetActive(enabled);
            window.transform.SetParent(parent);
        }

        public void HideWindow(string windowKey)
        {
            if (_createdWindows.TryGetValue(windowKey, out var window))
            {
                window.gameObject.SetActive(false);
            }
        }

        public void Initialize()
        {
            _mainCanvasTransform = _screenManager.MainCanvasContainer.MainCanvasTransform;
        }
    }
}