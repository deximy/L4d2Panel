using L4d2PanelBackend.ViewModels;

namespace L4d2PanelBackend.Services
{
    public interface IProcessService
    {
        public bool is_running { get; }

        public Task<Guid> RunServer(Action<string> callback);

        public void ExecCommand(string command);

        public Task<bool> StopServer();

        public Task<List<Guid>> GetProcessStateId(int count);

        public Task<ServerProcessState> GetProcessState(Guid id);

        public Task<List<ServerProcessState>> GetProcessState(List<Guid> id_list);
    }
}
