using BILab.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BILab.DataAccess.EntitiesConfigurations {
    public class SheduleConfig : IEntityTypeConfiguration<Shedule> {
        public void Configure(EntityTypeBuilder<Shedule> builder) {
            builder.ToTable("Schedules");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}
