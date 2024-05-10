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

            //builder.HasData(new TypeOfDay() {
            //    Id = Guid.Parse("6b82916e-57a2-41f7-90ee-f9e92f71a2aa"),
            //    Name = "Рабочий"
            //},
            //builder.HasData(new TypeOfDay() {
            //    Id = Guid.Parse("b7a016ca-5ab7-45ab-9803-7ec57355ad70"),
            //    Name = "Выходной"
            //}, builder.HasData(new TypeOfDay() {
            //    Id = Guid.Parse("81febc80-59ad-47e9-979d-25f685c4dda4"),
            //    Name = "Больничный"
            //})));
        }
    }
}
