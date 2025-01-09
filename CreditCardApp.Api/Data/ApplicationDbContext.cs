using CreditCardApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditCardApp.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet para la entidad Card
        public DbSet<Card> Cards { get; set; } = null!;
    }
}
