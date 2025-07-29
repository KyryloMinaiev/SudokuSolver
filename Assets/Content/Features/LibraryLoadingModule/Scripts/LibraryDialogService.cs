using Core.FileBrowserModule.Scripts;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LibraryDialogService : ILibraryDialogService
    {
        private const string LibraryFileExtension = "sdklbr";
        private readonly IFileBrowserService _fileBrowserService;
        
        public LibraryDialogService(IFileBrowserService fileBrowserService)
        {
            _fileBrowserService = fileBrowserService;
        }
        
        public string OpenLibraryFileDialog()
        {
            return _fileBrowserService.OpenFileBrowser("Select Library", "", LibraryFileExtension);
        }

        public string SaveNewLibraryDialog()
        {
            return _fileBrowserService.SaveFileBrowser("Create Library", string.Empty, "New Library", LibraryFileExtension);
        }
    }
}