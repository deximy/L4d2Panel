namespace L4d2PanelBackend.API.ViewModels
{
    public class ServerState
    {
        public bool is_running { get; set; }

        public ServerProcessState? process_state { get; set; }

        public ServerGameState? game_state { get; set; }
    }
}
