using System;
using System.Collections.Generic;
using Content.Features.ScreensModule.Scripts;
using Core.AssetLoaderModule.Scripts;
using Core.DIContainer.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Content.Features.WindowManagerModule.Scripts
{
    public struct WindowData
    {
        public readonly BaseUIWindow Window;
        public readonly GameObject WindowContainer;
        
        public WindowData(BaseUIWindow window, GameObject windowContainer)
        {
            Window = window;
            WindowContainer = windowContainer;
        }
    }
    
    public class WindowManager : IWindowManager, IInitializable
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IScreenManager _screenManager;
        private readonly Dictionary<string, WindowData> _createdWindows;
        
        private Transform _mainCanvasTransform;

        public WindowManager(IAssetLoader assetLoader, IScreenManager screenManager)
        {
            _assetLoader = assetLoader;
            _screenManager = screenManager;
            _createdWindows = new Dictionary<string, WindowData>();
        }
        
        public async UniTask PrepareWindow<T>(string windowKey, Action<T> onWindowReady = null) where T : BaseUIWindow
        {
            if (_createdWindows.ContainsKey(windowKey))
            {
                return;
            }
            
            GameObject windowPrefab = await _assetLoader.LoadAssetAsync<GameObject>(windowKey);
            GameObject windowContainer = CreateWindowContainer(windowKey);
            GameObject windowObject = UnityEngine.Object.Instantiate(windowPrefab, windowContainer.transform);
            T window = windowObject.GetComponent<T>();
            if (window != null)
            {
                SetupNewWindow(windowKey, windowContainer, window, onWindowReady);
            }
            else
            {
                DestroyWindowContainer(windowContainer);
            }
        }

        private GameObject CreateWindowContainer(string windowKey)
        {
            GameObject windowContainer = new GameObject($"Container-{windowKey}");
            windowContainer.transform.SetParent(_mainCanvasTransform);
            windowContainer.transform.localScale = Vector3.one;
            windowContainer.transform.localPosition = Vector3.zero;
            windowContainer.SetActive(false);
            return windowContainer;
        }

        private void DestroyWindowContainer(GameObject windowContainer)
        {
            UnityEngine.Object.Destroy(windowContainer);
        }

        private void SetupNewWindow<T>(string windowKey, GameObject windowContainer, T window, Action<T> onWindowReady = null) where T : BaseUIWindow
        {
            _createdWindows[windowKey] = new WindowData(window, windowContainer);
            HideWindow(windowKey);
            onWindowReady?.Invoke(window);
        }

        public TWindow ShowWindow<TWindow>(string windowKey, BaseUIScreen parentScreen) where TWindow : BaseUIWindow
        {
            if (!parentScreen.IsOpened)
            {
                return null;
            }
            
            if (!_createdWindows.TryGetValue(windowKey, out var windowData))
            {
                return null;
            }
            
            SetupWindowContainer(windowData.WindowContainer, parentScreen.transform, true);
            return (TWindow)windowData.Window;
        }

        private void SetupWindowContainer(GameObject windowContainer, Transform parent, bool enabled)
        {
            windowContainer.gameObject.SetActive(enabled);
            windowContainer.transform.SetParent(parent);
        }

        public void HideWindow(string windowKey)
        {
            if (_createdWindows.TryGetValue(windowKey, out var windowData))
            {
                windowData.WindowContainer.SetActive(false);
            }
        }

        public void Initialize()
        {
            _mainCanvasTransform = _screenManager.MainCanvasContainer.MainCanvasTransform;
        }
    }
}