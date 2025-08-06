using Content.Features.UIModule.Scripts;

namespace Core.ColorSchemeModule.Scripts
{
    public interface IColorSchemePropertyContainer
    {
        ReactiveProperty<ColorScheme> ColorScheme { get; }
    }
}