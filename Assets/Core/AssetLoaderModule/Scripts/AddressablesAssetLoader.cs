using System;
using System.Collections.Generic;
using Core.LoadingServiceModule.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Core.AssetLoaderModule.Scripts
{
    public class AddressablesAssetLoader : IAssetLoader
    {
        private readonly ILoadingService _loadingService;
        private readonly Dictionary<Type, Stack<ILoadingTask>> _loadingTasksPool;

        private readonly HashSet<Object> _loadedAssets;
        private readonly Dictionary<string, Object> _loadedAssetsPaths;

        public AddressablesAssetLoader(ILoadingService loadingService)
        {
            _loadingService = loadingService;
            _loadingTasksPool = new Dictionary<Type, Stack<ILoadingTask>>();
            _loadedAssets = new HashSet<Object>();
            _loadedAssetsPaths = new Dictionary<string, Object>();
        }

        private AddressablesAssetLoadingTask<T> GetTaskFromStack<T>(Stack<ILoadingTask> stack) where T : Object
        {
            if (stack.Count == 0)
            {
                return new AddressablesAssetLoadingTask<T>();
            }

            return (AddressablesAssetLoadingTask<T>)stack.Pop();
        }

        private AddressablesAssetLoadingTask<T> GetLoadingTask<T>() where T : Object
        {
            Type type = typeof(T);

            if (!_loadingTasksPool.ContainsKey(type))
            {
                _loadingTasksPool[type] = new Stack<ILoadingTask>();
            }

            return GetTaskFromStack<T>(_loadingTasksPool[type]);
        }

        private void ReturnTaskToPool<T>(ILoadingTask task) where T : Object
        {
            Type type = typeof(T);
            _loadingTasksPool[type].Push(task);
        }

        public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
        {
            if (_loadedAssetsPaths.TryGetValue(path, out var asset) && asset != null)
            {
                return (T)asset;
            }

            AddressablesAssetLoadingTask<T> task = GetLoadingTask<T>();
            task.Setup(path);

            var completionSource = AutoResetUniTaskCompletionSource<T>.Create();
            _loadingService.EnqueueLoadingTask(task, () => { completionSource.TrySetResult(task.Result); });

            T result = await completionSource.Task;
            _loadedAssets.Add(result);
            _loadedAssetsPaths[path] = result;
            task.Clean();
            ReturnTaskToPool<T>(task);
            return result;
        }

        T IAssetLoader.LoadAsset<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            return handle.WaitForCompletion();
        }

        public void UnloadAsset(Object asset)
        {
            if (_loadedAssets.Contains(asset))
            {
                _loadedAssets.Remove(asset);
                Addressables.Release(asset);
            }
        }
    }
}