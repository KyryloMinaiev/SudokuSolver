using Content.Features.UIModule.Scripts;

namespace Content.Features.LoadingScreenModule.Scripts
{
    public class LoadingScreenViewModel
    {
        public ReactiveProperty<float> TaskProgress { get; } = new ReactiveProperty<float>();
        public ReactiveProperty<string> TaskDescription { get; } = new ReactiveProperty<string>();
    }
}