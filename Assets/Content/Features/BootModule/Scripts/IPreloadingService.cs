using Cysharp.Threading.Tasks;

namespace Content.Features.BootModule.Scripts
{
    public interface IPreloadingService
    {
        UniTask Preload();
    }
}