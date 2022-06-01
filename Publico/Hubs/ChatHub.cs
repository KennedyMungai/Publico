using Microsoft.AspNetCore.SignalR;

using Publico.Models;

namespace Publico.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(Message message) => await Clients.All.SendAsync("ReceiveMessage", message);
}