using _01.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(x =>
            {
                x.HasKey(s => s.Id);
                x.Property(s => s.AccountNumber).HasMaxLength(30);
                x.HasOne(s => s.Card)
                    .WithOne();
            });
        }
    }
}
