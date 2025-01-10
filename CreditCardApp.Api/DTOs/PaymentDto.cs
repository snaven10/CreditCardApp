namespace CreditCardApp.Api.DTOs
{
    public class PaymentDto
    {
        public int CardId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
