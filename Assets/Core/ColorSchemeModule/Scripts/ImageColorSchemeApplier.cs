using UnityEngine;
using UnityEngine.UI;

namespace Core.ColorSchemeModule.Scripts
{
    [RequireComponent(typeof(Image))]
    public class ImageColorSchemeApplier : BaseColorSchemeApplier
    {
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        
        protected override void ApplyColor(Color color)
        {
            _image.color = color;
        }
    }
}