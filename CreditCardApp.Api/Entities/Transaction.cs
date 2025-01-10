namespace CreditCardApp.Api.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public string TransactionType { get; set; } = string.Empty; // 'Purchase' o 'Payment'
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }

        // Relación con Card
        public Card Card { get; set; } = null!;
    }
}
