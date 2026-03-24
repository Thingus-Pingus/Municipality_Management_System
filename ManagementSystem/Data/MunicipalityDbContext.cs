using Microsoft.EntityFrameworkCore;
using ManagementSystem.Models;

namespace ManagementSystem.Data
{
    public class MunicipalityDbContext : DbContext
    {
        public MunicipalityDbContext(DbContextOptions<MunicipalityDbContext> options) : base(options) { }

        // Define DbSet properties for each model
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Enforce unique constraints
            modelBuilder.Entity<Citizen>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Staff>()
                .HasIndex(s => s.Email)
                .IsUnique();

            // Define relationships
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Citizen)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CitizenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Citizen)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.CitizenId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
