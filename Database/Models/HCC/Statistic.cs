using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace PotatoServer.Database.Models.HCC
{
    public class Statistic
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int NumberOfMistakes { get; set; }
        public string ControlName { get; set; }
        public int AppearanceRating { get; set; }
        public int ComfortRating { get; set; }
    }

    public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
    {
        public void Configure(EntityTypeBuilder<Statistic> builder)
        {
        }
    }
}
