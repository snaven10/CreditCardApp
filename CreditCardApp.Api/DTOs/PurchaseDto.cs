namespace CreditCardApp.Api.DTOs
{
    public class PurchaseDto
    {
        public int CardId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
