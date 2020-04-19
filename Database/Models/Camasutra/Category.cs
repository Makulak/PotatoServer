using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PotatoServer.Database.Models.Camasutra
{
    [Table("Camasutra_Category")]
    public class Category : IBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Position> Positions { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(u => u.Name)
                   .IsUnique();

            builder.Property(x => x.Created).HasDefaultValueSql("getdate()");
        }
    }
}
