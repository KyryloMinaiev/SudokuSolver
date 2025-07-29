using SFB;

namespace Core.FileBrowserModule.Scripts
{
    public class StandaloneFileBrowserService : IFileBrowserService
    {
        public string OpenFileBrowser(string title, string directory, string extension)
        {
            string[] results = StandaloneFileBrowser.OpenFilePanel(title, directory, extension, false);
            if (results.Length > 0)
            {
                return results[0];
            }
            
            return string.Empty;
        }

        public string OpenFolderBrowser(string title, string directory)
        {
            string[] results = StandaloneFileBrowser.OpenFolderPanel(title, directory, false);
            if (results.Length > 0)
            {
                return results[0];
            }
            
            return string.Empty;
        }

        public string SaveFileBrowser(string title, string directory, string defaultName, string extension)
        {
            return StandaloneFileBrowser.SaveFilePanel(title, directory, defaultName, extension);
        }
    }
}