using CryptoExchange.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBalance> Balances { get; set; }
        public DbSet<BalanceTransaction> BalanceTransactions { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentData> PaymentsData { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBalance>().HasKey(x => new { x.CurrencyId, x.UserId });
            modelBuilder.Entity<UserBalance>().HasOne(x => x.Currency).WithMany(x => x.Balances);
            modelBuilder.Entity<UserBalance>().HasOne(x => x.User).WithMany(x => x.Balances);

            modelBuilder.Entity<CurrencyNetwork>().HasKey(x => new { x.CurrencyId, x.NetworkId });
            modelBuilder.Entity<CurrencyNetwork>().HasOne(x => x.Currency).WithMany(x => x.Networks);
            modelBuilder.Entity<CurrencyNetwork>().HasOne(x => x.Network).WithMany(x => x.Currencies);

            modelBuilder.Entity<BalanceTransaction>().Property(x => x.Created).HasDefaultValueSql("now()");
            modelBuilder.Entity<Payment>().Property(x => x.Created).HasDefaultValueSql("now()");
            modelBuilder.Entity<User>().Property(x => x.Created).HasDefaultValueSql("now()");


        }
    }
}
