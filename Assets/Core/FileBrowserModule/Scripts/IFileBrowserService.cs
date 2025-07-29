namespace Core.FileBrowserModule.Scripts
{
    public interface IFileBrowserService
    {
        string OpenFileBrowser(string title, string directory, string extension);
        string OpenFolderBrowser(string title, string directory);
        string SaveFileBrowser(string title, string directory, string defaultName , string extension);
    }
}