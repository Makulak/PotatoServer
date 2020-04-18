using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PotatoServer.Database.Models.CleverWord;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PotatoServer.Database.Models.CleverWord
{
    [Table("CleverWord_Word")]
    public class Word : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? Created { get; set; }
        public DateTime? Changed { get; set; }
    }
}

public class WordConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.HasIndex(u => u.Name)
               .IsUnique();

        builder.Property(x => x.Created).HasDefaultValueSql("getdate()");
    }
}