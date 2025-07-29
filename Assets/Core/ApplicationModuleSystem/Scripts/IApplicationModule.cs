namespace Core.ApplicationModuleSystem.Scripts
{
    public interface IApplicationModule
    {
        void Activate();
        void Deactivate();
    }
}