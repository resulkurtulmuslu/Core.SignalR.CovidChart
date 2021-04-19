using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Core.SignalR.CovidChart.API.Hubs
{
    public class CovidHub : Hub
    {
        public async Task GetCovidList()
        {
            await Clients.All.SendAsync("ReceiveCovidList","data");
        }
    }
}
