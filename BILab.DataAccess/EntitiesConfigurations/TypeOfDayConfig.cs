using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class TypeOfDayConfig : IEntityTypeConfiguration<TypeOfDay> {
        public void Configure(EntityTypeBuilder<TypeOfDay> builder) {
            builder.ToTable("TypesOfDay");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Constants.MaxLenOfName);

            builder.HasMany(x => x.Shedules)
                .WithOne(x => x.TypeOfDay)
                .HasForeignKey(x => x.TypeOfDayId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new TypeOfDay() {
                Id = Guid.NewGuid(),
                Name = "Рабочий"
            },
            builder.HasData(new TypeOfDay() {
                Id = Guid.NewGuid(),
                Name = "Выходной"
            }, builder.HasData(new TypeOfDay() {
                Id = Guid.NewGuid(),
                Name = "Больничный"
            })));
        }
    }
}
