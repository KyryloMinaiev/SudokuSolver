using Core.LoadingServiceModule.Scripts;

namespace Core.SceneLoadingModule.Scripts
{
    public interface ISceneLoadingTaskFactory
    {
        ILoadingTask CreateSceneLoadingTask(string sceneName, bool isAdditive);
        ILoadingTask CreateSceneUnloadingTask(string sceneName);
    }
}