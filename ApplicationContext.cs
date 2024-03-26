using CryptoExchange.Entities;
using CryptoExchange.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange
{

    public class ApplicationContext : DbContext
    {

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentData> PaymentsData { get; set; }
        public DbSet<CurrencyNetwork> CurrencyNetworks { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<WithdrawalCash> WithdrawalsCash { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

            Database.Migrate();
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<CurrencyNetwork>().HasKey(x => new { x.CurrencyId, x.NetworkId });
            modelBuilder.Entity<CurrencyNetwork>().HasOne(x => x.Currency).WithMany(x => x.Networks);
            modelBuilder.Entity<CurrencyNetwork>().HasOne(x => x.Network).WithMany(x => x.Currencies);

            modelBuilder.Entity<Payment>().Property(x => x.Created).HasDefaultValueSql("now()");
            modelBuilder.Entity<User>().Property(x => x.Created).HasDefaultValueSql("now()");
            modelBuilder.Entity<Withdrawal>().Property(x => x.Created).HasDefaultValueSql("now()");

        }
    }
}
