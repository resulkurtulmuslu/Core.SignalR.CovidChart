using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SignalR.CovidChart.API.Models
{
    public enum ECity
    {
        Istanbul = 1,
        Ankara = 2,
        Izmir = 3,
        Sivas = 4,
        Giresun = 5
    }

    public class Covid
    {
        public int Id { get; set; }

        public ECity City { get; set; }

        public int Count { get; set; }

        public DateTime CovidDate { get; set; }
    }
}
