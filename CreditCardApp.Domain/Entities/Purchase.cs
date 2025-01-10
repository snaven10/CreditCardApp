namespace CreditCardApp.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        // Relación con Card
        public Card Card { get; set; } = null!;
    }
}
