using Content.Features.SolverApplicationStateMachine.Scripts;
using Core.ApplicationStateMachineModule.Scripts;
using Core.DIContainer.Scripts;
using UnityEngine;

namespace Content.Features.SolverApplication.Scripts
{
    public class SolverApplication : MonoBehaviour
    {
        [SerializeField]
        private DIContainer _diContainerPrefab;
        
        private DIContainer _diContainer;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            CreateApplicationModuleContainer();
            CreateGlobalModule();
        }

        private void CreateApplicationModuleContainer()
        {
            _diContainer = Instantiate(_diContainerPrefab);
        }

        private void CreateGlobalModule()
        {
            _diContainer.Install<GlobalModuleInstaller>();
        }

        private void Start()
        {
            _diContainer.Get<IApplicationStateMachine>().EnterState<BootApplicationState>();
        }
    }
}