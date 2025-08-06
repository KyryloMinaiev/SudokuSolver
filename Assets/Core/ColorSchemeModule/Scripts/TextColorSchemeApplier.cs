using TMPro;
using UnityEngine;

namespace Core.ColorSchemeModule.Scripts
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextColorSchemeApplier : BaseColorSchemeApplier
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        protected override void ApplyColor(Color color)
        {
            _text.color = color;
        }
    }
}