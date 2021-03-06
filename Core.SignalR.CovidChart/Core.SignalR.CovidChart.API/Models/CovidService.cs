using Core.SignalR.CovidChart.API.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SignalR.CovidChart.API.Models
{
    public class CovidService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<CovidHub> _hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _context.Covids.AsQueryable();
        }

        public async Task SaveCovid(Covid covid)
        {
            await _context.Covids.AddAsync(covid);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidChartList());
        }

        public List<CovidChart> GetCovidChartList()
        {
            List<CovidChart> covidChart = new List<CovidChart>();

            using(var command = _context.Database
                .GetDbConnection()
                .CreateCommand())
            {
                command.CommandText = "select tarih,[1],[2],[3]," +
                    "[4],[5] from (select[City],[Count], CAST([CovidDate] as date)" +
                    " as tarih from Covids) as covidT PIVOT (SUM(Count) for City " +
                    "IN([1],[2],[3],[4],[5])) as pTable order by tarih asc";

                command.CommandType = System.Data.CommandType.Text;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CovidChart c = new CovidChart();

                        c.CovidDate = reader.GetDateTime(0).ToShortDateString();

                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                c.Count.Add(0);
                            }
                            else
                            {
                                c.Count.Add(reader.GetInt32(x));
                            }
                        });

                        covidChart.Add(c);
                    }
                }

                _context.Database.CloseConnection();
            }

            return covidChart;
        }
    }
}
