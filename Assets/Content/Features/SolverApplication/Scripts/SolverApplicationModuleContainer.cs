using System;
using System.Collections.Generic;
using Core.ApplicationModuleSystem.Scripts;
using UnityEngine;

namespace Content.Features.SolverApplication.Scripts
{
    public class SolverApplicationModuleContainer : IApplicationModuleContainer
    {
        private readonly Dictionary<Type, IApplicationModule> _modules = new Dictionary<Type, IApplicationModule>();
        
        public T BindModule<T>(T module) where T : IApplicationModule
        {
            if (!_modules.ContainsKey(typeof(T)))
            {
                _modules.Add(typeof(T), module);
            }
            
            return (T)_modules[typeof(T)];
        }

        public void ActivateModule<T>() where T : IApplicationModule
        {
            if(_modules.TryGetValue(typeof(T), out var module))
            {
                module.Activate();
            }
            else
            {
                Debug.LogError($"[SolverApplicationModuleContainer] Module of type {typeof(T)} not found!");
            }
        }

        public void DeactivateModule<T>() where T : IApplicationModule
        {
            if(_modules.TryGetValue(typeof(T), out var module))
            {
                module.Deactivate();
            }
            else
            {
                Debug.LogError($"[SolverApplicationModuleContainer] Module of type {typeof(T)} not found!");
            }
        }
    }
}