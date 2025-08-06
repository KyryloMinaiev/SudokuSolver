using Core.DIContainer.Scripts;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts
{
    public abstract class BaseColorSchemeApplier : MonoBehaviour
    {
        [SerializeField] private int _colorRoleID;
        
        private IColorSchemePropertyContainer _colorSchemePropertyContainer;
        
        [Inject]
        public void Initialize(IColorSchemePropertyContainer colorSchemePropertyContainer)
        {
            _colorSchemePropertyContainer = colorSchemePropertyContainer;
        }

        private void OnEnable()
        {
            _colorSchemePropertyContainer.ColorScheme.Subscribe(OnColorSchemeChanged);
        }

        private void OnColorSchemeChanged(ColorScheme colorScheme)
        {
            Color color = colorScheme.GetColor(_colorRoleID);
            ApplyColor(color);
        }

        protected abstract void ApplyColor(Color color);

        private void OnDisable()
        {
            _colorSchemePropertyContainer.ColorScheme.Unsubscribe(OnColorSchemeChanged);
        }
    }
}