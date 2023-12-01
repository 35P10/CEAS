using Microsoft.AspNetCore.SignalR;

namespace FrontEnd.Models.Hubs
{
    public class CodeSharing : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
