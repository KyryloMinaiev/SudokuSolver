namespace Core.ApplicationModuleSystem.Scripts
{
    public interface IApplicationModuleContainer
    {
        T BindModule<T>(T module) where T : IApplicationModule;
        void ActivateModule<T>() where T : IApplicationModule;
        void DeactivateModule<T>() where T : IApplicationModule;
    }
}