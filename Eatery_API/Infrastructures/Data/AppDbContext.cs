using Eatery_API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Username)
                .IsUnique();
            modelBuilder.Entity<Account>()
               .HasIndex(a => a.Email)
               .IsUnique();
            modelBuilder.Entity<Account>()
               .HasIndex(a => a.PhoneNumber)
               .IsUnique();
        }
    }
}
