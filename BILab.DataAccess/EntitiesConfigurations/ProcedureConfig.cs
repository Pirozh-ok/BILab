using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class ProcedureConfig : IEntityTypeConfiguration<Procedure> {
        public void Configure(EntityTypeBuilder<Procedure> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(Constants.MaxLenOfName);

            builder.HasOne(x => x.SpecialOffer)
                .WithMany(x => x.Procedures)
                .HasForeignKey(x => x.SpecialOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
