namespace L4d2PanelBackend.API.Services
{
    public class BaseFileService
    {
        public static string RelativePathToAbsulotePath(string? base_path, string? relative_path)
        {
            string target_path = Path.Combine(base_path ?? "/l4d2/left4dead2", relative_path ?? ".");
            target_path = Path.GetFullPath(target_path);
            if (Path.GetRelativePath("/l4d2/left4dead2", target_path).StartsWith(".."))
            {
                throw new InvalidOperationException($"Invalid target path: {target_path}");
            }
            return target_path;
        }
    }
}
