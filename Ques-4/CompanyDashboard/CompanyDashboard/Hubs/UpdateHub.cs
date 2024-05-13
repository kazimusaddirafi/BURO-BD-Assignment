using Microsoft.AspNetCore.SignalR;

namespace CompanyDashboard.Hubs
{
    public class UpdateHub: Hub
    {
        public async Task SendUpdate()
        {
            await Clients.All.SendAsync("ReceiveUpdate");
        }
    }
}
