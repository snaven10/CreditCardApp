namespace CreditCardApp.Api.DTOs
{
    public class CardDto
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
