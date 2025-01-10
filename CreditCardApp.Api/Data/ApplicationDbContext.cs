using CreditCardApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApp.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Purchase> Purchases { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaci�n Card -> Purchase
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Purchases)
                .WithOne(p => p.Card)
                .HasForeignKey(p => p.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relaci�n Card -> Payment
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.Card)
                .HasForeignKey(p => p.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relaci�n Card -> Transaction
            modelBuilder.Entity<Card>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Card)
                .HasForeignKey(t => t.CardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuraci�n espec�fica para Transaction
            modelBuilder.Entity<Transaction>()
                .Property(t => t.TransactionType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
