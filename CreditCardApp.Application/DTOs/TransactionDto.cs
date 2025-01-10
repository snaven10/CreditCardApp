namespace CreditCardApp.Application.DTOs
{
    public class TransactionDto
    {
        public int CardId { get; set; }
        public string TransactionType { get; set; } = string.Empty; // 'Purchase' o 'Payment'
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }
}
