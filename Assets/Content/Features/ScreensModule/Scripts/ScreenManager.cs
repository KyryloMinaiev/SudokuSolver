using System;
using System.Collections.Generic;
using Core.DIContainer.Scripts;
using Core.AssetLoaderModule.Scripts;
using Cysharp.Threading.Tasks;
using Global.Scripts.Generated;
using UnityEngine;

namespace Content.Features.ScreensModule.Scripts
{
    public class ScreenManager : IScreenManager, IInitializable
    {
        private readonly DIContainer _container;
        private readonly IAssetLoader _assetLoader;
        private readonly HashSet<string> _preparedScreens;
        private readonly Dictionary<Type, BaseUIScreen> _createdScreens;
        private MainCanvasContainer _mainCanvasContainer;

        public ScreenManager(IAssetLoader assetLoader, DIContainer container)
        {
            _assetLoader = assetLoader;
            _container = container;
            _createdScreens = new Dictionary<Type, BaseUIScreen>();
            _preparedScreens = new HashSet<string>();
        }
        
        public MainCanvasContainer MainCanvasContainer => _mainCanvasContainer;

        public async UniTask PrepareScreen<T>(string screenPrefabPath, Action<T> onScreenReady = null) where T : BaseUIScreen
        {
            if (_preparedScreens.Contains(screenPrefabPath))
            {
                return;
            }
            
            GameObject screenPrefab = await _assetLoader.LoadAssetAsync<GameObject>(screenPrefabPath);
            T screen = _container.InstantiateComponent<T>(screenPrefab, _mainCanvasContainer.MainCanvasTransform);
            if (screen != null)
            {
                SetupScreen(screen, screenPrefabPath, onScreenReady);
            }
            else
            {
                Debug.LogError($"[ScreenManager] Screen {screenPrefabPath} does not have {typeof(T)} component!");
                DestroyScreenObject(screen.gameObject);
            }
        }

        private void SetupScreen<T>(T screen, string screenPrefabPath, Action<T> onScreenReady) where T : BaseUIScreen
        {
            _preparedScreens.Add(screenPrefabPath);
            _createdScreens[typeof(T)] = screen;
            HideScreen<T>();
            onScreenReady?.Invoke(screen);
        }

        private void DestroyScreenObject(GameObject screenObject)
        {
            UnityEngine.Object.Destroy(screenObject);
        }

        public T ShowScreen<T>() where T : BaseUIScreen
        {
            if (_createdScreens.TryGetValue(typeof(T), out var screen))
            {
                screen.OpenScreen();
                return (T)screen;
            }

            return null;
        }

        public bool TryGetScreen<T>(out T screen) where T : BaseUIScreen
        {
            if (_createdScreens.TryGetValue(typeof(T), out var baseScreen))
            {
                screen = (T)baseScreen;
                return true;
            }
            
            screen = null;
            return false;
        }

        public void HideScreen<T>() where T : BaseUIScreen
        {
            if (_createdScreens.TryGetValue(typeof(T), out var screen))
            {
                screen.CloseScreen();
            }
        }

        public void Initialize()
        {
            var mainCanvasContainerPrefab = _assetLoader.LoadAsset<GameObject>(Address.UIScreens.MainCanvasContainer);
            _mainCanvasContainer = _container.InstantiateComponent<MainCanvasContainer>(mainCanvasContainerPrefab);
        }
    }
}