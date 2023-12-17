using CryptoCalculator.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoCalculator
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<UserBalance> Balances { get; set; }
        public DbSet<BalanceTransaction> BalanceTransactions { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBalance>().HasKey(x=> new {x.CurrencyId, x.UserId});
            modelBuilder.Entity<UserBalance>().HasOne(x => x.Currency).WithMany(x => x.Balances);
            modelBuilder.Entity<UserBalance>().HasOne(x => x.User).WithMany(x => x.Balances);


            modelBuilder.Entity<BalanceTransaction>().Property(x => x.Created).HasDefaultValueSql("now()");
        }
    }
}
