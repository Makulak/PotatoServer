using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace PotatoServer.Database.Models
{
    public class CardSet
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        IEnumerable<BlackCard> BlackCards { get; set; }
        IEnumerable<WhiteCard> WhiteCards { get; set; }
    }

    public class CardSetConfiguration : IEntityTypeConfiguration<CardSet>
    {
        public void Configure(EntityTypeBuilder<CardSet> builder)
        {
        }
    }
}
