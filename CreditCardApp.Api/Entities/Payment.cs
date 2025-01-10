namespace CreditCardApp.Api.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        // Relación con Card
        public Card Card { get; set; } = null!;
    }
}
