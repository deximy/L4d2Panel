namespace L4d2PanelBackend.ViewModels
{
    public class FileList
    {
        public IEnumerable<string> files { get; set; }

        public IEnumerable<string> directories { get; set; }
    }
}
