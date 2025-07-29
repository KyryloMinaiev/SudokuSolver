using Content.Features.WindowManagerModule.Scripts;
using Cysharp.Threading.Tasks;
using Global.Scripts.Generated;

namespace Content.Features.BootModule.Scripts
{
    public class WindowsPreloadingService : IPreloadingService
    {
        private readonly IWindowManager _windowManager;
        
        public WindowsPreloadingService(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        
        public async UniTask Preload()
        {
            var windowsKeys = Address.Windows.AllKeys;
            foreach (var windowsKey in windowsKeys)
            {
                await _windowManager.PrepareWindow<BaseUIWindow>(windowsKey);
            }
        }
    }
}