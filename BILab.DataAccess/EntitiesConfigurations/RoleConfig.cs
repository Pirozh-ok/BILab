using BILab.Domain;
using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class RoleConfig : IEntityTypeConfiguration<Role> {
        public void Configure(EntityTypeBuilder<Role> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Constants.MaxLenOfName);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

            builder.HasData(
                new Role {
                    Id = Guid.NewGuid(),
                    Name = Constants.NameRoleAdmin,
                    NormalizedName = Constants.NameRoleAdmin.ToUpper()
                },

                new Role {
                    Id = Guid.NewGuid(),
                    Name = Constants.NameRoleUser,
                    NormalizedName = Constants.NameRoleUser.ToUpper()
                },

                new Role {
                    Id = Guid.NewGuid(),
                    Name = Constants.NameRoleCustomer,
                    NormalizedName = Constants.NameRoleCustomer.ToUpper()
                });
        }
    }
}
