namespace Core.DIContainer.Scripts
{
    public interface IInstaller<T>
    {
        void Install(DIContainer container);
    }
}