namespace L4d2PanelBackend.API.ViewModels
{
    public class FileList
    {
        public IEnumerable<string> files { get; set; }

        public IEnumerable<string> directories { get; set; }
    }
}
