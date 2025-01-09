namespace CreditCardApp.Api.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
