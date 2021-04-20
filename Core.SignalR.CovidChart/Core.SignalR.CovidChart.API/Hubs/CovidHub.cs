using Core.SignalR.CovidChart.API.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Core.SignalR.CovidChart.API.Hubs
{
    public class CovidHub : Hub
    {
        private readonly CovidService _service;

        public CovidHub(CovidService service)
        {
            _service = service;
        }

        public async Task GetCovidList()
        {
            await Clients.All.SendAsync("ReceiveCovidList", _service.GetCovidChartList());
        }
    }
}
