using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models.Camasutra;
using PotatoServer.Database.Models.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PotatoServer.Database.Models.Camasutra
{
    [Table("Camasutra_Position")]
    public class Position : IBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }

        public int CategoryId { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
    }

    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasIndex(u => u.Name)
                   .IsUnique();

            builder.Property(x => x.Created).HasDefaultValueSql("getdate()");
        }
    }
}
