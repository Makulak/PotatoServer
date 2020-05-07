using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PotatoServer.Database;
using PotatoServer.Database.Models.HCC;
using PotatoServer.Services.Mapping;
using PotatoServer.ViewModels.HCC;

namespace PotatoServer.Controllers.HCC
{
    [Route("api/hcc/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly StatisticMapper _mapper;

        public StatisticsController(DatabaseContext context,
            StatisticMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Statistic>> PostStatistic(StatisticPostVm statisticVm)
        {
            var statistic = _mapper.MapToStatistic(statisticVm);
            _context.Statistics.Add(statistic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
