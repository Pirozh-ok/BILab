using BILab.DataAccess.EntitiesConfigurations;
using BILab.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace BILab.DataAccess {
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid> {
        public ApplicationDbContext() {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shedule> Shedules { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<TypeOfDay> TypesOfDays { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AdressConfig());
            builder.ApplyConfiguration(new ProcedureConfig());
            builder.ApplyConfiguration(new RecordConfig());
            builder.ApplyConfiguration(new SheduleConfig());
            builder.ApplyConfiguration(new SpecialOfferConfig());
            builder.ApplyConfiguration(new TypeOfDayConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new RoleConfig());
        }
    }
}
