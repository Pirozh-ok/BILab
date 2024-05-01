using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class UserConfig : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLenOfName);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(Constants.MaxLenOfName);

            builder.Property(x => x.Patronymic)
                .HasDefaultValue(string.Empty)
                .HasMaxLength(Constants.MaxLenOfName);

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.Property(x => x.RegisterDate)
                .HasDefaultValueSql("getdate()");

            builder.HasMany(x => x.CustomerRecords)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.EmployeeRecords)
                .WithOne(x => x.Employer)
                .HasForeignKey(x => x.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Shedules)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
