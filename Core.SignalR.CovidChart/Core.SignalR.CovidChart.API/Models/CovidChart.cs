using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SignalR.CovidChart.API.Models
{
    public class CovidChart
    {
        public CovidChart()
        {
            Count = new List<int>();
        }

        public string CovidDate { get; set; }

        public List<int> Count { get; set; }
    }
}
