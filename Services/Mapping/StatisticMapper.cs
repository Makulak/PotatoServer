using PotatoServer.Database.Models.HCC;
using PotatoServer.ViewModels.HCC;

namespace PotatoServer.Services.Mapping
{
    public class StatisticMapper
    {
        public Statistic MapToStatistic(StatisticPostVm statisticVm)
        {
            if (statisticVm == null)
                return null;

            return new Statistic
            {
                UserId = statisticVm.UserId,
                StartDateTime = statisticVm.StartDateTime.Value,
                EndDateTime = statisticVm.EndDateTime.Value,
                NumberOfMistakes = statisticVm.NumberOfMistakes.Value,
                ControlName = statisticVm.ControlName,
                AppearanceRating = statisticVm.AppearanceRating.Value,
                ComfortRating = statisticVm.ComfortRating.Value
            };
        }
    }
}
