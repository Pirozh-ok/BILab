using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class SpecialOfferConfig : IEntityTypeConfiguration<SpecialOffer> {
        public void Configure(EntityTypeBuilder<SpecialOffer> builder) {
            builder.ToTable("SpecialOffers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Detail)
                .HasMaxLength(Constants.MaxLenOfDetail);
        }
    }
}
