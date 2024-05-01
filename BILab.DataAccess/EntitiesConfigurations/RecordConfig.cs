using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class RecordConfig : IEntityTypeConfiguration<Record> {
        public void Configure(EntityTypeBuilder<Record> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Adress)
                .WithMany(x => x.Records)
                .HasForeignKey(x => x.AdressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
