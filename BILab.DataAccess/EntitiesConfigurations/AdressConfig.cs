using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class AdressConfig : IEntityTypeConfiguration<Adress> {
        public void Configure(EntityTypeBuilder<Adress> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.City)
                .IsRequired();

            builder.Property(x => x.Street)
               .IsRequired();

            builder.Property(x => x.HouseNumber)
               .IsRequired();

            builder.Property(x => x.ApartmentNumber)
               .IsRequired();
        }
    }
}
