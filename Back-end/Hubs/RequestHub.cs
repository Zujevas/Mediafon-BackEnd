using Microsoft.AspNetCore.SignalR;

namespace Back_end.Hubs
{
    public class RequestHub :Hub
    {
        public async Task NotifyRequestUpdate()
        {
            await Clients.All.SendAsync("ReceiveRequestUpdate");
        }
    }
}
