using System;
using Cysharp.Threading.Tasks;

namespace Content.Features.ScreensModule.Scripts
{
    public interface IScreenManager
    {
        MainCanvasContainer MainCanvasContainer { get; }
        UniTask PrepareScreen<T>(string screenPrefab, Action<T> onScreenReady = null) where T : BaseUIScreen;
        T ShowScreen<T>() where T : BaseUIScreen;
        bool TryGetScreen<T>(out T screen) where T : BaseUIScreen;
        void HideScreen<T>() where T : BaseUIScreen;
    }
}