using System;
using Content.Features.SolverApplicationClosingModule.Scripts;
using Content.Features.SolverApplicationStateMachine.Scripts;
using Content.Features.UIModule.Scripts;
using Core.ApplicationStateMachineModule.Scripts;
using UnityEngine.Events;

namespace Content.Features.MainScreenModule.Scripts.ApplicationControlPanel
{
    public class ApplicationControlPanelViewModel : IDisposable
    {
        private readonly IApplicationClosingService _applicationClosingService;
        private readonly IApplicationStateMachine _applicationStateMachine;
        private readonly IMainScreenStateController _mainScreenStateController;

        public readonly ReactiveProperty<bool> ShowBackButton = new ();
        
        public UnityAction HomeButtonCommand { get; }
        public UnityAction CloseApplicationCommand { get; }
        public UnityAction LoadLibraryCommand { get; }
        public UnityAction BackButtonCommand { get; }

        public ApplicationControlPanelViewModel(IApplicationClosingService applicationClosingService,
            IApplicationStateMachine applicationStateMachine, IMainScreenStateController mainScreenStateController)
        {
            _applicationClosingService = applicationClosingService;
            _applicationStateMachine = applicationStateMachine;
            _mainScreenStateController = mainScreenStateController;
            _mainScreenStateController.OnStateChanged += OnMainScreenStateChanged;

            HomeButtonCommand = HomeButton;
            LoadLibraryCommand = LoadLibrary;
            CloseApplicationCommand = CloseApplication;
            BackButtonCommand = BackButton;
        }

        private void HomeButton()
        {
            _applicationStateMachine.EnterState<MainScreenApplicationState>();
        }

        private void CloseApplication()
        {
            _applicationClosingService.CloseApplication();
        }

        private void LoadLibrary()
        {
            _applicationStateMachine.EnterState<LibraryLoadingApplicationState>();
        }
        
        private void BackButton()
        {
            _mainScreenStateController.PopState();
        }

        private void OnMainScreenStateChanged()
        {
            ShowBackButton.Value = _mainScreenStateController.HasSavedStates();
        }

        public void Dispose()
        {
            _mainScreenStateController.OnStateChanged -= OnMainScreenStateChanged;
        }
    }
}