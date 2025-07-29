namespace Content.Features.LibraryLoadingModule.Scripts
{
    public interface ILibraryFlowService
    {
        void LoadLibrary(string filePath);
        void CreateNewLibrary(string filePath);
    }
}