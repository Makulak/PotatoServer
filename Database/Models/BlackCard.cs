using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PotatoServer.Database.Models
{
    public class BlackCard
    {
        string Text { get; set; }
        int Pick { get; set; }

        CardSet CardSet { get; set; }
        int CardSetId { get; set; }
    }

    public class BlackCardConfiguration : IEntityTypeConfiguration<BlackCard>
    {
        public void Configure(EntityTypeBuilder<BlackCard> builder)
        {
        }
    }
}
