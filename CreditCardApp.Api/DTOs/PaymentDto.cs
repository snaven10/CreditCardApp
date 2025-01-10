namespace CreditCardApp.Api.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; } // Identificador para actualizaciones
        public int CardId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
