namespace L4d2PanelBackend.Services
{
    public class FileListService
    {
        public static List<string> GetDirectoriesList(string target_path)
        {
            return Directory.GetDirectories(target_path).Select(x => Path.GetRelativePath(target_path, x)).OrderBy(x => x).ToList();
        }

        public static List<string> GetFilesList(string target_path)
        {
            return Directory.GetFiles(target_path).Select(x => Path.GetRelativePath(target_path, x)).OrderBy(x => x).ToList();
        }
    }
}
