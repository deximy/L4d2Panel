using Microsoft.AspNetCore.SignalR;

namespace L4d2PanelBackend.API.Hubs
{
    public class MessageHub : Hub
    {
        public delegate void MessageHandler(string message);
        MessageHandler? message_handler = null;

        public async Task DistributeMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task HandleMessage(string message)
        {
            message_handler?.Invoke(message);
        }
    }
}
