using Cysharp.Threading.Tasks;

namespace Core.AssetLoaderModule.Scripts
{
    public interface IAssetLoader
    {
        UniTask<T> LoadAssetAsync<T>(string path) where T : UnityEngine.Object;
        T LoadAsset<T>(string path) where T : UnityEngine.Object;
        void UnloadAsset(UnityEngine.Object asset);
    }
}