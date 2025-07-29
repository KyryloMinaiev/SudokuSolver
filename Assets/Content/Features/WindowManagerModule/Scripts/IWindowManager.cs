using System;
using Content.Features.ScreensModule.Scripts;
using Cysharp.Threading.Tasks;

namespace Content.Features.WindowManagerModule.Scripts
{
    public interface IWindowManager
    {
        UniTask PrepareWindow<T>(string windowKey, Action<T> onWindowReady = null) where T : BaseUIWindow;
        TWindow ShowWindow<TWindow>(string windowKey, BaseUIScreen parentScreen) where TWindow : BaseUIWindow;
        void HideWindow(string windowKey);
    }
}