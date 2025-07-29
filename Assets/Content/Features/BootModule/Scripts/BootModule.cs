using System.Collections.Generic;
using Content.Features.SolverApplicationStateMachine.Scripts;
using Core.ApplicationModuleSystem.Scripts;
using Core.ApplicationStateMachineModule.Scripts;
using Core.SceneLoadingModule.Scripts;
using Cysharp.Threading.Tasks;
using Global.Scripts.Generated;

namespace Content.Features.BootModule.Scripts
{
    public class BootModule : IApplicationModule
    {
        private readonly ISceneLoadingService _sceneLoadingService;
        private readonly IApplicationStateMachine _applicationStateMachine;
        private readonly List<IPreloadingService> _preloadingServices;

        public BootModule(ISceneLoadingService sceneLoadingService, IApplicationStateMachine applicationStateMachine,
            List<IPreloadingService> preloadingServices)
        {
            _sceneLoadingService = sceneLoadingService;
            _applicationStateMachine = applicationStateMachine;
            _preloadingServices = preloadingServices;
        }

        public async void Activate()
        {
            await PerformPreloading();
        }

        private async UniTask PerformPreloading()
        {
            foreach (var preloadingService in _preloadingServices)
            {
                await preloadingService.Preload();
            }

            _sceneLoadingService.LoadSceneAsMain(Address.Scenes.MainScene, OnMainSceneLoaded);
        }

        private void OnMainSceneLoaded()
        {
            _applicationStateMachine.EnterState<LibraryLoadingApplicationState>();
        }

        public void Deactivate()
        {
        }
    }
}